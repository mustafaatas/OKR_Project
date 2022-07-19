using API.DTO.KeyResult;
using API.Validators;
using AutoMapper;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyResultController : ControllerBase
    {
        private readonly IKeyResultService _keyResultService;
        private readonly IMapper _mapper;

        public KeyResultController(IKeyResultService keyResultService, IMapper mapper)
        {
            _mapper = mapper;
            _keyResultService = keyResultService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<KeyResultDTO>>> GetAllKeyResults()
        {
            var keyResults = await _keyResultService.GetAllKeyResults();
            var keyResultResources = _mapper.Map<IEnumerable<KeyResult>, IEnumerable<KeyResultDTO>>(keyResults);

            return Ok(keyResultResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KeyResultDTO>> GetKeyResultById(int id)
        {
            var keyResult = await _keyResultService.GetKeyResultById(id);
            var keyResultResource = _mapper.Map<KeyResult, KeyResultDTO>(keyResult);

            return Ok(keyResultResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<KeyResultDTO>> CreateKeyResult([FromBody] SaveKeyResultDTO saveKeyResultResource)
        {
            var validator = new SaveKeyResultResourceValidator();
            var validationResult = await validator.ValidateAsync(saveKeyResultResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var keyResultToCreate = _mapper.Map<SaveKeyResultDTO, KeyResult>(saveKeyResultResource);
            var newKeyResult = await _keyResultService.CreateKeyResult(keyResultToCreate);
            var keyResult = await _keyResultService.GetKeyResultById(newKeyResult.Id);
            var keyResultResource = _mapper.Map<KeyResult, KeyResultDTO>(keyResult);
            return Ok(keyResultResource);
           

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<KeyResultDTO>> UpdateKeyResult(int id, [FromBody] SaveKeyResultDTO saveKeyResultResource)
        {
            var validator = new SaveKeyResultResourceValidator();
            var validationResult = await validator.ValidateAsync(saveKeyResultResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var keyResultToBeUpdated = await _keyResultService.GetKeyResultById(id);

            if (keyResultToBeUpdated == null)
                return NotFound();

            var keyResult = _mapper.Map<SaveKeyResultDTO, KeyResult>(saveKeyResultResource);
            await _keyResultService.UpdateKeyResult(keyResultToBeUpdated, keyResult);
            var updatedKeyResult = await _keyResultService.GetKeyResultById(id);
            var updatedKeyResultResource = _mapper.Map<KeyResult, KeyResultDTO>(updatedKeyResult);
            return Ok(updatedKeyResultResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKeyResult(int id)
        {
            var keyResult = await _keyResultService.GetKeyResultById(id);
            await _keyResultService.DeleteKeyResult(keyResult);

            return NoContent();
        }
    }
}