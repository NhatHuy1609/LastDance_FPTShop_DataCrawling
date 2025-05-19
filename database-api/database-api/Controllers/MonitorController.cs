using database_api.Dtos.Monitor;
using database_api.Interfaces;
using database_api.Mappers;
using database_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var monitorResult = await _monitorRepository.GetAllMonitors(queryParams);
            
            // Convert monitors to DTOs while preserving pagination data
            var result = new PagedResult<MonitorDto>
            {
                Items = monitorResult.Items.Select(m => m.ToDto()),
                TotalItems = monitorResult.TotalItems,
                PageNumber = monitorResult.PageNumber,
                PageSize = monitorResult.PageSize,
                TotalPages = monitorResult.TotalPages
            };
            
            return Ok(result);
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