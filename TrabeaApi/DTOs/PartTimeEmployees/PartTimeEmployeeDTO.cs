namespace TrabeaApi.DTOs.PartTimeEmployees;

public class PartTimeEmployeeDTO
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PersonalEmail { get; set; }
    public string WorkEmail { get; set; }
    public string PersonalPhoneNumber { get; set; }
    public DateTime JoinDate { get; set; }
}
