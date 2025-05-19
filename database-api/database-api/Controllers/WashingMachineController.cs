using AutoMapper;
using database_api.Dtos.WashingMachine;
using database_api.Entities;
using database_api.Interfaces;
using database_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace database_api.Controllers;

[ApiController]
[Route("[controller]")]
public class WashingMachineController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<WashingMachineController> _logger;
    private readonly IWashingMachineRepository _washingMachineRepository;

    public WashingMachineController(
        IMapper mapper,
        ILogger<WashingMachineController> logger,
        IWashingMachineRepository washingMachineRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _washingMachineRepository = washingMachineRepository;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<WashingMachineDto>>> GetWashingMachineAsync(
        [FromQuery] int limit = 10,
        [FromQuery] string? cursor = null)
    {
        var washingMachines = await _washingMachineRepository.GetWashingMachineAsync(limit, cursor);
        var washingMachineDtos = _mapper.Map<PaginatedResult<WashingMachineDto>>(washingMachines);
        return Ok(washingMachineDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WashingMachineDto>> GetWashingMachineByIdAsync(int id)
    {
        var washingMachine = await _washingMachineRepository.GetWashingMachineByIdAsync(id);
        if (washingMachine == null)
        {
            return NotFound();
        }
        var washingMachineDto = _mapper.Map<WashingMachineDto>(washingMachine);
        return Ok(washingMachineDto);
    }

    [HttpPost]
    public async Task<ActionResult<WashingMachineDto>> AddWashingMachineAsync(CreateWashingMachineDto createWashingMachineDto)
    {
        var washingMachine = _mapper.Map<WashingMachine>(createWashingMachineDto);
        var createdWashingMachineDto = await _washingMachineRepository.AddWashingMachineAsync(washingMachine);
        var washingMachineDto = _mapper.Map<WashingMachineDto>(createdWashingMachineDto);
        return Ok(washingMachineDto);
    }
}