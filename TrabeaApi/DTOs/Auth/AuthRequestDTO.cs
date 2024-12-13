using System.ComponentModel.DataAnnotations;

namespace TrabeaApi.DTOs.Auth;

public class AuthRequestDTO
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public int RoleId { get; set; }
}
