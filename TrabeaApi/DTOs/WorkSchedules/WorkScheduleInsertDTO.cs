using System.ComponentModel.DataAnnotations;

namespace TrabeaApi.DTOs.WorkSchedules;

public class WorkScheduleInsertDTO
{
    [Required]
    public DateTime WorkDate { get; set; }
    [Required]
    public int ShiftId { get; set; }
    public string? UserEmail { get; set; }
}
