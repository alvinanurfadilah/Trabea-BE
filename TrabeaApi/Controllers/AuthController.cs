using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrabeaApi.DTOs.Auth;
using TrabeaApi.Services;
using TrabeaDataAccess.Models;

namespace TrabeaApi.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly UserService _userService;

    public AuthController(AuthService authService, UserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
            var user = _userService.Get(email);
            var fullNameEmployee = user.Employees.Select(e => e.FirstName + " " + e.LastName).ToList().FirstOrDefault();
            var fullNamePartTimeEmployee = user.PartTimeEmployees.Select(ptEmp => ptEmp.FirstName + " " + ptEmp.LastName).ToList().FirstOrDefault();

            var roles = user.Roles.Select(r => new Role()
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();

            string fullName = "";
            if (roles.Any(r => r.Id == 1 || r.Id == 2))
            {
                fullName = fullNameEmployee;
            }
            else if (roles.Any(r => r.Id == 3))
            {
                fullName = fullNamePartTimeEmployee;
            }

            var response = new AuthResponseDTO()
            {
                Token = _authService.CreateToken(user).Token,
                Email = email,
                FullName = fullName,
                Roles = roles
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Login(AuthRequestDTO dto)
    {
        var response = _authService.GetToken(dto);
        return Ok(response);
    }
}
