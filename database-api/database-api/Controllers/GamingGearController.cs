using AutoMapper;
using database_api.Dtos.GamingGear;
using database_api.Entities;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace database_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamingGearController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GamingGearController> _logger;
        private readonly IGamingGearRepository _gamingGearRepository;

        public GamingGearController(IMapper mapper, ILogger<GamingGearController> logger, IGamingGearRepository gamingGearRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _gamingGearRepository = gamingGearRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<GamingGearDto>>> GetGamingGearsAsync(
            [FromQuery] int limit = 10,
            [FromQuery] string? cursor = null,
            [FromQuery] string? name = null,
            [FromQuery] string? category = null,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool isDescending = false)
        {
            var gamingGears = await _gamingGearRepository.GetGamingGearsAsync(limit, cursor, name, category, minPrice, maxPrice, sortBy, isDescending);
            var gamingGearDtos = _mapper.Map<PaginatedResult<GamingGearDto>>(gamingGears);
            return Ok(gamingGearDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GamingGearDto>> GetGamingGearByIdAsync(int id)
        {
            try
            {
                var gamingGear = await _gamingGearRepository.GetGamingGearByIdAsync(id);
                var gamingGearDto = _mapper.Map<GamingGearDto>(gamingGear);
                return Ok(gamingGearDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GamingGearDto>> AddGamingGearAsync(CreateGamingGearDto createGamingGearDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gamingGear = _mapper.Map<GamingGear>(createGamingGearDto);
            var createdGamingGear = await _gamingGearRepository.AddGamingGearAsync(gamingGear);
            var gamingGearDto = _mapper.Map<GamingGearDto>(createdGamingGear);

            return CreatedAtAction(nameof(GetGamingGearByIdAsync), new { id = gamingGearDto.Id }, gamingGearDto);
        }
    }
}
