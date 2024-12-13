using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabeaApi.Services;

namespace TrabeaApi.Controllers;

[ApiController]
[Route("api/v1/role")]
//[Authorize]
public class RoleController : ControllerBase
{
    private readonly RoleService _service;

    public RoleController(RoleService service)
    {
        _service = service;
    }

    [HttpGet("role-dropdown")]
    public IActionResult RoleDropdown()
    {
        var model = _service.RoleDropdown();
        return Ok(model);
    }
}
