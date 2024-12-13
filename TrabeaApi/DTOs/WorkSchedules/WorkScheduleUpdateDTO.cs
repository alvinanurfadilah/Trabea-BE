namespace TrabeaApi.DTOs.WorkSchedules;

public class WorkScheduleUpdateDTO
{
    public int Id { get; set; }
    public bool IsApproved { get; set; }
    public string? UserEmail { get; set; }
}
