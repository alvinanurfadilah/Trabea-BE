using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabeaApi.DTOs.Employees;
using TrabeaApi.Services;

namespace TrabeaApi.Controllers;

[ApiController]
[Route("api/v1/employee")]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeeController(EmployeeService service)
    {
        _service = service;
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
    public IActionResult Insert(EmployeeInsertDTO dto)
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
    public IActionResult Update(EmployeeUpdateDTO dto)
    {
        try
        {
            _service.Update(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }
}
