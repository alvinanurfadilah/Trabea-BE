using System.ComponentModel.DataAnnotations;

namespace TrabeaApi.DTOs.PartTimeEmployees;

public class PartTimeEmployeeUpdateDTO
{
    public int Id { get; set; }
    [Required]
    [StringLength(maximumLength: 25)]
    public string FirstName { get; set; } = null!;
    [StringLength(maximumLength: 50)]
    public string? LastName { get; set; }
    [Required]
    [StringLength(maximumLength: 100)]
    [EmailAddress]
    public string PersonalEmail { get; set; } = null!;
    [Required]
    [StringLength(maximumLength: 20)]
    [Phone]
    public string PersonalPhoneNumber { get; set; } = null!;
    [Required]
    [StringLength(maximumLength: 50)]
    public string LastEducation { get; set; } = null!;
    [Required]
    [StringLength(maximumLength: 255)]
    public string Address { get; set; } = null!;
    [StringLength(maximumLength: 50)]
    public string? OnGoingEducation { get; set; }
}
