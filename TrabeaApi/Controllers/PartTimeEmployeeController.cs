using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabeaApi.DTOs.PartTimeEmployees;
using TrabeaApi.Services;

namespace TrabeaApi.Controllers;

[ApiController]
[Route("api/v1/part-time-employee")]
[Authorize(Roles = "Administrator")]
public class PartTimeEmployeeController : ControllerBase
{
    private readonly PartTimeEmployeeService _service;

    public PartTimeEmployeeController(PartTimeEmployeeService service)
    {
        _service = service;
    }

    [HttpGet("education-dropdown")]
    public IActionResult Get()
    {
        try
        {
            var model = _service.EducationDropdown();
            return Ok(model);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpGet]
    public IActionResult Get(int pageNumber = 1, string? fullName = "")
    {
        try
        {
            var model = _service.Get(pageNumber, fullName);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            var model = _service.Get(id);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpPost]
    public IActionResult Insert(PartTimeEmployeeInsertDTO dto)
    {
        try
        {
            _service.Insert(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(PartTimeEmployeeUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);
            return Ok();
        } catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }
}
