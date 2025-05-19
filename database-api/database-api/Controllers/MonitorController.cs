using database_api.Dtos.Monitor;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace database_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitorController : ControllerBase
    {
        private readonly IMonitorRepository _monitorRepository;
        private readonly IMapper _mapper;

        public MonitorController(IMonitorRepository monitorRepository, IMapper mapper)
        {
            _monitorRepository = monitorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<MonitorDto>>> GetMonitorsAsync(
            [FromQuery] int limit = 10,
            [FromQuery] string? cursor = null,
            [FromQuery] string? name = null,
            [FromQuery] string? category = null,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool isDescending = false)
        {
            var monitors = await _monitorRepository.GetMonitorsAsync(limit, cursor, name, category, minPrice, maxPrice, sortBy, isDescending);
            var monitorDtos = _mapper.Map<PaginatedResult<MonitorDto>>(monitors);
            return Ok(monitorDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonitorDto>> GetMonitorById(int id)
        {
            try
            {
                var monitor = await _monitorRepository.GetMonitorById(id);
                var monitorDto = _mapper.Map<MonitorDto>(monitor);
                return Ok(monitorDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MonitorDto>> CreateMonitor(CreateMonitorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var monitor = _mapper.Map<Entities.Monitor>(request);
            var createdMonitor = await _monitorRepository.CreateMonitor(monitor);
            var monitorDto = _mapper.Map<MonitorDto>(createdMonitor);

            return CreatedAtAction(nameof(GetMonitorById), new { id = monitorDto.Id }, monitorDto);
        }
    }
}