using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrabeaApi.DTOs.Auth;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaApi.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public AuthResponseDTO CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var item in user.Employees)
        {
            claims.Add(new Claim(ClaimTypes.Name, item.FirstName + " " + item.LastName));
        }

        foreach (var item in user.PartTimeEmployees)
        {
            claims.Add(new Claim(ClaimTypes.Name, item.FirstName + " " + item.LastName));
        }

        foreach (var item in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, item.Name));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value ?? throw new NullReferenceException("")));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        var getUser = _userRepository.Get(user.Email);
        var fullNameEmployee = getUser.Employees.Select(e => e.FirstName + " " + e.LastName).ToList().FirstOrDefault();
        var fullNamePartTimeEmployee = getUser.PartTimeEmployees.Select(ptEmp => ptEmp.FirstName + " " + ptEmp.LastName).ToList().FirstOrDefault();

        var roles = getUser.Roles.Select(r => new Role()
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

        return new AuthResponseDTO()
        {
            Token = serializedToken,
            Email = user.Email,
            FullName = fullName,
            Roles = roles
        };
    }

    public AuthResponseDTO GetToken(AuthRequestDTO dto)
    {
        var model = _userRepository.Get(dto.Email);
        bool isCorrectEmail = dto.Email == model.Email;
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(dto.Password, model.Password);
        bool isCorrectRole = model.Roles.Any(r => r.Id == dto.RoleId);

        if (isCorrectEmail && isCorrectPassword && isCorrectRole)
        {
            return CreateToken(model);
        }

        throw new NullReferenceException("Email or Password or Role is incorrect!");
    }
}
