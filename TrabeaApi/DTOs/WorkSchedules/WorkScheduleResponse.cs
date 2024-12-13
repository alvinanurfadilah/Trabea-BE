namespace TrabeaApi.DTOs.WorkSchedules;

public class WorkScheduleResponse
{
    public DateTime WorkDate { get; set; }
    public List<WorkScheduleDTO> Shifts { get; set; }
}
