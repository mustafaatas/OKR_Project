using API.DTO;
using API.Validators;
using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            _mapper = mapper;
            _artistService = artistService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtists();
            var artistResources = _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistDTO>>(artists);

            return Ok(artistResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtistById(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            var artistResource = _mapper.Map<Artist, ArtistDTO>(artist);

            return Ok(artistResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<ArtistDTO>> CreateArtist([FromBody] SaveArtistDTO saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var artistToCreate = _mapper.Map<SaveArtistDTO, Artist>(saveArtistResource);
            var newArtist = await _artistService.CreateArtist(artistToCreate);
            var artist = await _artistService.GetArtistById(newArtist.Id);
            var artistResource = _mapper.Map<Artist, ArtistDTO>(artist);

            return Ok(artistResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistDTO>> UpdateArtist(int id, [FromBody] SaveArtistDTO saveArtistResource)
        {
            var validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var artistToBeUpdated = await _artistService.GetArtistById(id);

            if (artistToBeUpdated == null)
                return NotFound();

            var artist = _mapper.Map<SaveArtistDTO, Artist>(saveArtistResource);
            await _artistService.UpdateArtist(artistToBeUpdated, artist);
            var updatedArtist = await _artistService.GetArtistById(id);
            var updatedArtistResource = _mapper.Map<Artist, ArtistDTO>(updatedArtist);
            return Ok(updatedArtistResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            await _artistService.DeleteArtist(artist);

            return NoContent();
        }
    }
}
