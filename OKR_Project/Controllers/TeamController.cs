using API.DTO.Team;
using API.Validators;
using AutoMapper;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _mapper = mapper;
            _teamService = teamService;
        }

        [HttpGet("")]
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

        [HttpPost("")]
        public async Task<ActionResult<TeamDTO>> CreateTeam([FromBody] SaveTeamDTO saveTeamResource)
        {
            var validator = new SaveTeamResourceValidator();
            var validationResult = await validator.ValidateAsync(saveTeamResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var teamToCreate = _mapper.Map<SaveTeamDTO, Team>(saveTeamResource);
            var newTeam = await _teamService.CreateTeam(teamToCreate);
            var team = await _teamService.GetTeamById(newTeam.Id);
            var teamResource = _mapper.Map<Team, TeamDTO>(team);
            return Ok(teamResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TeamDTO>> UpdateTeam(int id, [FromBody] SaveTeamDTO saveTeamResource)
        {
            var validator = new SaveTeamResourceValidator();
            var validationResult = await validator.ValidateAsync(saveTeamResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var teamToBeUpdated = await _teamService.GetTeamById(id);

            if (teamToBeUpdated == null)
                return NotFound();

            var team = _mapper.Map<SaveTeamDTO, Team>(saveTeamResource);
            await _teamService.UpdateTeam(teamToBeUpdated, team);
            var updatedTeam = await _teamService.GetTeamById(id);
            var updatedTeamResource = _mapper.Map<Team, TeamDTO>(updatedTeam);
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
