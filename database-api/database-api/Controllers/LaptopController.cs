using AutoMapper;
using database_api.Dtos.Laptop;
using database_api.Entities;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace database_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaptopController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LaptopController> _logger;
        private readonly ILaptopRepository _laptopRepository;

        public LaptopController(
            IMapper mapper,
            ILogger<LaptopController> logger,
            ILaptopRepository laptopRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _laptopRepository = laptopRepository;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<LaptopDto>>> GetLaptopsAsync(
            [FromQuery] int limit = 10,
            [FromQuery] string? cursor = null)
        {
            var laptops = await _laptopRepository.GetLaptopsAsync(limit, cursor);
            var laptopDtos = _mapper.Map<PaginatedResult<LaptopDto>>(laptops);
            
            return Ok(laptopDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LaptopDto>> GetLaptopByIdAsync(int id)
        {
            var laptop = await _laptopRepository.GetLaptopByIdAsync(id);
            
            if (laptop == null)
            {
                return NotFound();
            }
            
            var laptopDto = _mapper.Map<LaptopDto>(laptop);
            return Ok(laptopDto);
        }

        [HttpPost]
        public async Task<ActionResult<LaptopDto>> AddLaptopAsync(CreateLaptopDto createLaptopDto)
        {
            var laptop = _mapper.Map<Laptop>(createLaptopDto);
            var createdLaptop = await _laptopRepository.AddLaptopAsync(laptop);
            
            var laptopDto = _mapper.Map<LaptopDto>(createdLaptop);
            return Ok(laptopDto);
        }
    }
} 