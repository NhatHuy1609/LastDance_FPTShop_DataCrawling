using database_api.Dtos.Monitor;
using database_api.Interfaces;
using database_api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace database_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitorController : ControllerBase
    {
        private readonly IMonitorRepository _monitorRepository;

        public MonitorController(IMonitorRepository monitorRepository)
        {
            _monitorRepository = monitorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMonitors([FromQuery] QueryParams queryParams)
        {
            var monitors = await _monitorRepository.GetAllMonitors(queryParams);
            return Ok(monitors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitorById(int id)
        {
            var monitor = await _monitorRepository.GetMonitorById(id);
            return Ok(monitor.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonitor(CreateMonitorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var monitor = request.ToEntityFromCreateMonitorRequest(); // Convert CreateMonitorRequest to Monitor entity
            var createdMonitor = await _monitorRepository.CreateMonitor(monitor);
            return CreatedAtAction(nameof(GetMonitorById), new { id = createdMonitor.Id }, createdMonitor.ToDto());
        }
    }
}