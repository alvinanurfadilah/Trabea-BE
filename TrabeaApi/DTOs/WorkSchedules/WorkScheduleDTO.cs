using TrabeaDataAccess.Models;

namespace TrabeaApi.DTOs.WorkSchedules;

public class WorkScheduleDTO
{
    public int ShiftId { get; set; }
    public int PartTimeEmployeeId { get; set; }
    public string PartTimeEmployee { get; set; }
}
