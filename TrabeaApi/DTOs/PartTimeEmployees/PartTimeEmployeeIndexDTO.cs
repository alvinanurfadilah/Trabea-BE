namespace TrabeaApi.DTOs.PartTimeEmployees;

public class PartTimeEmployeeIndexDTO
{
    public List<PartTimeEmployeeDTO> PartTimeEmployees { get; set; }
    public PaginationDTO Pagination { get; set; }
    public string FullName { get; set; }
}
