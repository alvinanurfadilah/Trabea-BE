namespace TrabeaApi.DTOs.Employees;

public class EmployeeIndexDTO
{
    public List<EmployeeDTO> Employees { get; set; }
    public PaginationDTO Pagination { get; set; }
    public string FullName { get; set; }
}
