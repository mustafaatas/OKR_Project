using API.DTO;
using API.DTO.Objective;
using API.Validators;
using AutoMapper;
using Core.Auth;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ObjectiveController : ControllerBase
    {
        private readonly IObjectiveService _objectiveService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IDepartmentService _departmentService;
        private readonly UserManager<User> _userManager;

        public ObjectiveController(IObjectiveService objectiveService, IMapper mapper, IUserService userService, ITeamService teamService, IDepartmentService departmentService, UserManager<User> userManager)
        {
            _mapper = mapper;
            _objectiveService = objectiveService;
            _userService = userService;
            _teamService = teamService;
            _departmentService = departmentService;
            _userManager = userManager;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ObjectiveDTO>>> GetAllObjectives()
        {
            var objectives = _objectiveService.GetAllObjectives();
            var objectivesResources = objectives.Select(k => new ObjectiveDTO
            {
                Id = k.Id,
                Title = k.Title,
                Owner = k.User.FirstName + " " + k.User.LastName,
                Description = k.Description,
                DepartmentName = k.Department.Name,
                //TeamId = k.Team.Id,
                //SurObjectiveId = k.SurObjective.Id,
                //KeyResults = k.KeyResults,
                //SubObjectives = k.SubObjectives
            }).ToList();

            return Ok(objectivesResources);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ObjectiveDTO>> GetObjectiveById(int id)
        {
            var objective = await _objectiveService.GetObjectiveById(id);
            var objectiveResource = _mapper.Map<Objective, ObjectiveDTO>(objective);

            return Ok(objectiveResource);
        }

        [HttpGet]
        public async Task<ActionResult<ObjectiveDTO>> GetObjectivesByUser()
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();

            
            var objectives = await _objectiveService.GetAllObjectives().Where(k => k.UserId == user.Id).ToListAsync();

            return Ok(objectives);
        }

        //[Authorize(Roles = "Admin, Leader")]
        [HttpGet]
        public async Task<ActionResult<ObjectiveDTO>> GetObjectivesByDepartment(HttpContext httpContext)
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();

            var department = await _departmentService.GetAllDepartments().Where(x => x.LeaderId == user.Id).FirstOrDefaultAsync();
            var objectives = department?.Objectives.ToList();

            return Ok(objectives);
        }

        //[Authorize(Roles = "Admin, Leader")]
        [HttpPost]
        public async Task<ActionResult<ObjectiveDTO>> CreateObjective([FromBody] SaveObjectiveDTO saveObjectiveResource)
        {
            try
            {
                var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
                var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();
                var department = await _departmentService.GetAllDepartments().Where(x => x.LeaderId == user.Id).FirstOrDefaultAsync();
                var role = await _userManager.GetRolesAsync(user);
                var roleName = role.FirstOrDefault();
                if (department.Id != saveObjectiveResource.DepartmentId && roleName != "Admin")
                {
                    return BadRequest(new Response { Status = "Error", Message = "You don't have authorize to this objective" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            var validator = new SaveObjectiveResourceValidator();
            var validationResult = await validator.ValidateAsync(saveObjectiveResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var objectiveToCreate = _mapper.Map<SaveObjectiveDTO, Objective>(saveObjectiveResource);
            //var user = await _userService.GetAllUsers().Where(x => x.Id == saveObjectiveResource.UserId).FirstOrDefaultAsync();
            var isSelectedTeam = _teamService.GetAllTeams().Any(x => x.Id == saveObjectiveResource.TeamId);
            var isSelectedDepartment = _departmentService.GetAllDepartments().Any(x => x.Id == saveObjectiveResource.DepartmentId);
            var teams = await _userService.GetAllUsers().Where(x => x.Id == saveObjectiveResource.UserId).Select(k => k.TeamUsers.Select(x => x.Team)).ToListAsync();
            var isTeamIdInTeamIds = teams.Any(k => k.Any(k => k.Id == objectiveToCreate.TeamId));

            if(!isTeamIdInTeamIds && saveObjectiveResource.TeamId != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "Cannot added objective to this team. Please select the team one of your belongs." });
            }

            if (!(isSelectedTeam || isSelectedDepartment))
            {
                return BadRequest(new Response { Status = "Error", Message = "Objective has to be part of department or team" });
            }

            var newObjective = await _objectiveService.CreateObjective(objectiveToCreate);
            var objective = await _objectiveService.GetObjectiveById(newObjective.Id);
            var objectiveResource = _mapper.Map<Objective, ObjectiveDTO>(objective);

            return Ok(objectiveResource);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ObjectiveDTO>> CreateSubObjective([FromBody] SaveSubObjectiveDTO saveSubObjectiveResource)
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();
            var department = await _departmentService.GetAllDepartments().Where(x => x.LeaderId == user.Id).FirstOrDefaultAsync();
            var role = await _userManager.GetRolesAsync(user);
            var roleName = role.FirstOrDefault();

            if (department?.Id != saveSubObjectiveResource.SurObjectiveId && roleName != "Admin")
            {
                return BadRequest(new Response { Status = "Error", Message = "You don't have authorize to this objective" });
            }

            var validator = new SaveSubObjectiveResourceValidator();
            var validationResult = await validator.ValidateAsync(saveSubObjectiveResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var isHaveKeyResults = _objectiveService.GetAllObjectives().Where(i => i.Id == saveSubObjectiveResource.SurObjectiveId).Include(i => i.KeyResults).FirstOrDefault().KeyResults.Any();
            if (isHaveKeyResults)
                return BadRequest(new Response { Status = "Error", Message = "Cannot added Subobjective when exist Key Results." });

            var subObjectiveToCreate = _mapper.Map<SaveSubObjectiveDTO, Objective>(saveSubObjectiveResource);
            var newSubObjective = await _objectiveService.CreateObjective(subObjectiveToCreate);
            var subObjective = await _objectiveService.GetObjectiveById(newSubObjective.Id);
            var subObjectiveResource = _mapper.Map<Objective, ObjectiveDTO>(subObjective);
            return Ok(subObjectiveResource);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ObjectiveDTO>> UpdateObjective(int id, [FromBody] SaveObjectiveDTO saveObjectiveResource)
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();
            var department = await _departmentService.GetAllDepartments().Where(x => x.LeaderId == user.Id).FirstOrDefaultAsync();

            if (department?.Id != saveObjectiveResource.DepartmentId)
            {
                return BadRequest(new Response { Status = "Error", Message = "You don't have authorize to this objective" });
            }

            var validator = new SaveObjectiveResourceValidator();
            var validationResult = await validator.ValidateAsync(saveObjectiveResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var objectiveToBeUpdated = await _objectiveService.GetObjectiveById(id);

            if (objectiveToBeUpdated == null)
                return NotFound();

            var objective = _mapper.Map<SaveObjectiveDTO, Objective>(saveObjectiveResource);
            await _objectiveService.UpdateObjective(objectiveToBeUpdated, objective);
            var updatedObjective = await _objectiveService.GetObjectiveById(id);
            var updatedObjectiveResource = _mapper.Map<Objective, ObjectiveDTO>(updatedObjective);
            return Ok(updatedObjectiveResource);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObjective(int id)
        {
            var objective = await _objectiveService.GetObjectiveById(id);
            await _objectiveService.DeleteObjective(objective);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteObjectiveByDepartment(int id)
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();
            var department = await _departmentService.GetAllDepartments().Where(x => x.LeaderId == user.Id).FirstOrDefaultAsync();
            var objective = department?.Objectives.Where(k => k.Id == 24).FirstOrDefault();

            if(objective == null)
            {
                return BadRequest(new Response { Status = "Error", Message = "You don't have authorize to this objective" });
            }

            var objectiveToDeleted = await _objectiveService.GetObjectiveById(objective.Id);
            await _objectiveService.DeleteObjective(objectiveToDeleted);

            return NoContent();
        }
    }
}