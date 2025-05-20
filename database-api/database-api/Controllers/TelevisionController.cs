using AutoMapper;
using database_api.Dtos.Television;
using database_api.Entities;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace database_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelevisionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TelevisionController> _logger;
        private readonly ITelevisionRepository _laptopRepository;

        public TelevisionController(
            IMapper mapper,
            ILogger<TelevisionController> logger,
            ITelevisionRepository laptopRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _laptopRepository = laptopRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<TelevisionDto>>> GetTelevisionsAsync(
            [FromQuery] int limit = 10,
            [FromQuery] string? cursor = null,
            [FromQuery] string? name = null,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool isDescending = false)
        {
            var laptops = await _laptopRepository.GetTelevisionsAsync(limit, cursor, name, minPrice, maxPrice, sortBy, isDescending); ;
            var laptopDtos = _mapper.Map<PaginatedResult<TelevisionDto>>(laptops);

            return Ok(laptopDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TelevisionDto>> GetTelevisionByIdAsync(int id)
        {
            var laptop = await _laptopRepository.GetTelevisionByIdAsync(id);

            if (laptop == null)
            {
                return NotFound();
            }

            var laptopDto = _mapper.Map<TelevisionDto>(laptop);
            return Ok(laptopDto);
        }

        [HttpPost]
        public async Task<ActionResult<TelevisionDto>> AddTelevisionAsync(CreateTelevisionDto createTelevisionDto)
        {
            var laptop = _mapper.Map<Television>(createTelevisionDto);
            var createdTelevision = await _laptopRepository.AddTelevisionAsync(laptop);

            var laptopDto = _mapper.Map<TelevisionDto>(createdTelevision);
            return Ok(laptopDto);
        }
    }
}
