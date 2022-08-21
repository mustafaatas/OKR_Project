using API.DTO;
using API.DTO.Objective;
using API.Validators;
using AutoMapper;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ObjectiveController : ControllerBase
    {
        private readonly IObjectiveService _objectiveService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;

        public ObjectiveController(IObjectiveService objectiveService, IMapper mapper, IUserService userService, ITeamService teamService)
        {
            _mapper = mapper;
            _objectiveService = objectiveService;
            _userService = userService;
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ObjectiveDTO>>> GetAllObjectives()
        {
            var objectives = _objectiveService.GetAllObjectives();
            var objectivesResources = _mapper.Map<IEnumerable<Objective>, IEnumerable<ObjectiveDTO>>(objectives);

            return Ok(objectivesResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ObjectiveDTO>> GetObjectiveById(int id)
        {
            var objective = await _objectiveService.GetObjectiveById(id);
            var objectiveResource = _mapper.Map<Objective, ObjectiveDTO>(objective);

            return Ok(objectiveResource);
        }

        [HttpPost]
        public async Task<ActionResult<ObjectiveDTO>> CreateObjective([FromBody] SaveObjectiveDTO saveObjectiveResource)
        {
            var validator = new SaveObjectiveResourceValidator();
            var validationResult = await validator.ValidateAsync(saveObjectiveResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var objectiveToCreate = _mapper.Map<SaveObjectiveDTO, Objective>(saveObjectiveResource);
            var user = await _userService.GetAllUsers().Where(x => x.Id == saveObjectiveResource.UserId).FirstOrDefaultAsync();
            var isSelectedTeam = _teamService.GetAllTeams().Any(x => x.Id == saveObjectiveResource.TeamId);
            var teams = await _userService.GetAllUsers().Where(x => x.Id == saveObjectiveResource.UserId).Select(k => k.TeamUsers.Select(x => x.Team)).ToListAsync();
            var isTeamIdInTeamIds = teams.Any(k => k.Any(k => k.Id == objectiveToCreate.TeamId));

            if(!isTeamIdInTeamIds)
            {
                return BadRequest(new Response { Status = "Error", Message = "Cannot added objective to this team. Please select the team one of your belongs." });
            }

            if (isSelectedTeam && objectiveToCreate.TeamId != null)
            {
                objectiveToCreate.DepartmentId = null;
            }
            else
            {
                objectiveToCreate.DepartmentId = user?.DepartmentId;
            }

            var newObjective = await _objectiveService.CreateObjective(objectiveToCreate);
            var objective = await _objectiveService.GetObjectiveById(newObjective.Id);
            var objectiveResource = _mapper.Map<Objective, ObjectiveDTO>(objective);

            return Ok(objectiveResource);
        }

        [HttpPost]
        public async Task<ActionResult<ObjectiveDTO>> CreateSubObjective([FromBody] SaveSubObjectiveDTO saveSubObjectiveResource)
        {
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ObjectiveDTO>> UpdateObjective(int id, [FromBody] SaveObjectiveDTO saveObjectiveResource)
        {
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObjective(int id)
        {
            var objective = await _objectiveService.GetObjectiveById(id);
            await _objectiveService.DeleteObjective(objective);

            return NoContent();
        }
    }
}