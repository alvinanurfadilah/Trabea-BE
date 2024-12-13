namespace TrabeaApi.DTOs.WorkSchedules;

public class WorkScheduleReviewResponseDTO
{
    public List<WorkScheduleReviewDTO> Reviews { get; set; }
    public PaginationDTO Pagination { get; set; }
    public string Name { get; set; }
    public int ShiftId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
