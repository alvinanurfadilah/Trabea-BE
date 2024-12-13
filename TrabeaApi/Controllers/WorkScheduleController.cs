using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrabeaApi.DTOs.WorkSchedules;
using TrabeaApi.Services;

namespace TrabeaApi.Controllers;

[ApiController]
[Route("api/v1/work-schedule")]
[Authorize(Roles = "Manager, PartTimer")]
public class WorkScheduleController : ControllerBase
{
    private readonly WorkScheduleService _service;

    public WorkScheduleController(WorkScheduleService service)
    {
        _service = service;
    }

    [HttpGet("shift-dropdown")]
    public IActionResult Get()
    {
        try
        {
            var model = _service.ShiftDropdown();
            return Ok(model);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpGet("{weekNumber}")]
    public IActionResult Get(int weekNumber = 1)
    {
        try
        {
            var model = _service.Get(weekNumber);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpGet("review")]
    public IActionResult Get(int shiftId, DateTime startDate, DateTime endDate, string? name = "", int pageNumber = 1)
    {
        try
        {
            if (endDate == default)
            {
                endDate = DateTime.MaxValue;
            }
            var model = _service.Get(name, shiftId, startDate, endDate, pageNumber);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpGet("get-by-id/{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var model = _service.GetById(id);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpPost]
    public IActionResult Insert(WorkScheduleInsertDTO dto)
    {
        try
        {
            var getUser = User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            dto.UserEmail = getUser;
            _service.Insert(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(WorkScheduleUpdateDTO dto)
    {
        try
        {
            var getUser = User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            dto.UserEmail = getUser;
            _service.Update(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }
}
