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

        public GamingGearController(IMapper mapper,ILogger<GamingGearController> logger,IGamingGearRepository gamingGearRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _gamingGearRepository = gamingGearRepository;
        }

        [HttpGet("cursor")]
        public async Task<ActionResult<PaginatedResult<GamingGearDto>>> GetGamingGearsAsync(
                       [FromQuery] int limit = 10,
                                  [FromQuery] string? cursor = null)
        {
            var gamingGears = await _gamingGearRepository.GetGamingGearsAsync(limit, cursor);
            var gamingGearDtos = _mapper.Map<PaginatedResult<GamingGearDto>>(gamingGears);

            return Ok(gamingGearDtos);
        }

        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<GamingGearDto>>> SearchGamingGearsAsync(
        [FromQuery] string? keyword = null,
        [FromQuery] string? category = null,
        [FromQuery] double? minPrice = null,
        [FromQuery] double? maxPrice = null,
        [FromQuery] bool? isAvailable = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
        {
            var result = await _gamingGearRepository.SearchGamingGearsAsync(
                keyword, category, minPrice, maxPrice, isAvailable, pageNumber, pageSize);

            var gamingGearDtos = _mapper.Map<PagedResult<GamingGearDto>>(result);
            return Ok(gamingGearDtos);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GamingGearDto>>> GetGamingGearsPagedAsync(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var gamingGears = await _gamingGearRepository.GetGamingGearsPagedAsync(pageNumber, pageSize);
            
            var gamingGearDtos = _mapper.Map<PagedResult<GamingGearDto>>(gamingGears);

            return Ok(gamingGearDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GamingGearDto>> GetGamingGearByIdAsync(int id)
        {
            var gamingGear = await _gamingGearRepository.GetGamingGearByIdAsync(id);

            if (gamingGear == null)
            {
                return NotFound();
            }

            var gamingGearDto = _mapper.Map<GamingGearDto>(gamingGear);
            return Ok(gamingGearDto);
        }

        [HttpPost]
        public async Task<ActionResult<GamingGearDto>> AddGamingGearAsync(CreateGamingGearDto createGamingGearDto)
        {
            var gamingGear = _mapper.Map<GamingGear>(createGamingGearDto);
            var createdGamingGear = await _gamingGearRepository.AddGamingGearAsync(gamingGear);

            var gamingGearDto = _mapper.Map<GamingGearDto>(createdGamingGear);
            return Ok(gamingGearDto);
        }
    }
}
