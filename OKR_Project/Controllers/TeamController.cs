using API.DTO.Team;
using API.Validators;
using AutoMapper;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(ITeamService teamService, IMapper mapper, IUserService userService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _teamService = teamService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAllTeams()
        {
            var teams = _teamService.GetAllTeams().ToList();
            var teamResources = _mapper.Map<IEnumerable<Team>, IEnumerable<TeamDTO>>(teams);

            return Ok(teamResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamById(id);
            var teamResource = _mapper.Map<Team, TeamDTO>(team);

            return Ok(teamResource);
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> CreateTeam([FromBody] SaveTeamDTO saveTeamResource)
        {
            var validator = new SaveTeamResourceValidator();
            var validationResult = await validator.ValidateAsync(saveTeamResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var teamToCreate = new Team { Name = saveTeamResource.Name };
            teamToCreate.SetTeamUsers(saveTeamResource.UserIds);

            _teamService.CreateTeam(teamToCreate);

            //_unitOfWork.Commit();

            var teamResource = _mapper.Map<Team, TeamDTO>(teamToCreate);
            return Ok(teamResource);

            //Controller ve service arasındaki ilişkiyi düzelt.
        }

        [HttpPut]
        public async Task<ActionResult<SaveTeamDTO>> UpdateTeam(SaveTeamDTO teamDto, int TeamId)
        {
            var validator = new SaveTeamResourceValidator();
            var validationResult = await validator.ValidateAsync(teamDto);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var teamToBeUpdated = await _teamService.GetTeamById(TeamId);

            if (teamToBeUpdated == null)
                return NotFound();

            teamToBeUpdated.Name = teamDto.Name;
            teamToBeUpdated.SetTeamUsers(teamDto.UserIds);

            _teamService.UpdateTeam(teamToBeUpdated);
            var updatedTeamResource = _mapper.Map<Team, TeamDTO>(teamToBeUpdated);
            return Ok(updatedTeamResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _teamService.GetTeamById(id);
            await _teamService.DeleteTeam(team);

            return NoContent();
        }
    }
}
