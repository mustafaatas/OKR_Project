using API.DTO.Objective;
using API.Validators;
using AutoMapper;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectiveController : ControllerBase
    {
        private readonly IObjectiveService _objectiveService;
        private readonly IMapper _mapper;

        public ObjectiveController(IObjectiveService objectiveService, IMapper mapper)
        {
            _mapper = mapper;
            _objectiveService = objectiveService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ObjectiveDTO>>> GetAllObjectives()
        {
            var objectives = await _objectiveService.GetAllObjectives();
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

        [HttpPost("")]
        public async Task<ActionResult<ObjectiveDTO>> CreateObjective([FromBody] SaveObjectiveDTO saveObjectiveResource)
        {
            var validator = new SaveObjectiveResourceValidator();
            var validationResult = await validator.ValidateAsync(saveObjectiveResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var objectiveToCreate = _mapper.Map<SaveObjectiveDTO, Objective>(saveObjectiveResource);
            var newObjective = await _objectiveService.CreateObjective(objectiveToCreate);
            var objective = await _objectiveService.GetObjectiveById(newObjective.Id);
            var objectiveResource = _mapper.Map<Objective, ObjectiveDTO>(objective);
            return Ok(objectiveResource);
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