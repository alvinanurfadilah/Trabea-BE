using TrabeaDataAccess.Models;

namespace TrabeaApi.DTOs.Auth;

public class AuthResponseDTO
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public List<Role> Roles { get; set; } = new List<Role>();
}
