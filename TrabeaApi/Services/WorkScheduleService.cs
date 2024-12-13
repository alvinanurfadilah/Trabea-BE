using Microsoft.AspNetCore.Mvc.Rendering;
using TrabeaApi.DTOs.WorkSchedules;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;
using static TrabeaApi.DTOs.Constant;

namespace TrabeaApi.Services;

public class WorkScheduleService
{
    private readonly IWorkScheduleRepository _repository;
    private readonly IWorkShiftRepository _shiftRepository;
    private readonly IUserRepository _userRepository;

    public WorkScheduleService(IWorkScheduleRepository repository, IWorkShiftRepository shiftRepository, IUserRepository userRepository)
    {
        _repository = repository;
        _shiftRepository = shiftRepository;
        _userRepository = userRepository;
    }

    public List<SelectListItem> ShiftDropdown()
    {
        var model = _shiftRepository.Get();
        return model.Select(shift => new SelectListItem()
        {
            Text = "Shift" + " " + shift.Id + " " + "(" + shift.StartTime + " " + "-" + " " + shift.EndTime + ")",
            Value = shift.Id.ToString()
        }).ToList();
    }

    private List<WorkScheduleDTO> GetSchedule(DateTime workDate)
    {
        var shifts = new WorkScheduleDTO[4];
        var model = _repository.Get(workDate).Select(ws => new WorkScheduleDTO()
        {
            ShiftId = ws.Shift.Id,
            PartTimeEmployeeId = ws.PartTimeEmployeeId,
            PartTimeEmployee = ws.PartTimeEmployee.FirstName + " " + ws.PartTimeEmployee.LastName
        }).ToList();

        foreach (var shift in model)
        {
            if (shift.ShiftId >= 1 && shift.ShiftId <= 4)
            {
                shifts[shift.ShiftId - 1] = shift;
            }
        }

        return shifts.ToList();
    }

    public List<WorkScheduleResponse> Get(int weekNumber)
    {
        DateTime startDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date.AddDays(7 * (weekNumber - 1));
        DateTime endDate = startDate.AddDays(6);

        var existingSchedules = _repository.Get(weekNumber);

        var completeWeek = Enumerable.Range(1, 5).Select(i => startDate.AddDays(i)).Select(date =>
        {
            var existingSchedule = existingSchedules.FirstOrDefault(ws => ws.WorkDate == date);
            return new WorkScheduleResponse()
            {
                WorkDate = date,
                Shifts = existingSchedule != null ? GetSchedule(existingSchedule.WorkDate) : GetSchedule(date)
            };
        }).ToList();

        return completeWeek;
    }

    public WorkScheduleReviewResponseDTO Get(string name, int shiftId, DateTime startDate, DateTime endDate, int pageNumber)
    {
        var model = _repository.Get(name, shiftId, startDate, endDate, pageNumber, PageSize).Select(ws => new WorkScheduleReviewDTO()
        {
            Id = ws.Id,
            Name = ws.PartTimeEmployee.FirstName + " " + ws.PartTimeEmployee.LastName,
            ProposedDate = ws.WorkDate.ToString("dd MMMM yyyy"),
            Shift = "Shift" + " " + ws.ShiftId + " " + "(" + ws.Shift.StartTime + " " + "-" + " " + ws.Shift.EndTime + ")"
        }).ToList();

        return new WorkScheduleReviewResponseDTO()
        {
            Reviews = model,
            Pagination = new DTOs.PaginationDTO()
            {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count(name, shiftId, startDate, endDate)
            },
            Name = name,
            ShiftId = shiftId,
            StartDate = startDate,
            EndDate = endDate,
        };
    }

    public WorkSchedule GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Insert(WorkScheduleInsertDTO dto)
    {
        var getUser = _userRepository.Get(dto.UserEmail);
        PartTimeEmployee getPTEmp = getUser.PartTimeEmployees.Where(ptEmp => ptEmp.WorkEmail == dto.UserEmail).FirstOrDefault();
        var model = new WorkSchedule()
        {
            WorkDate = dto.WorkDate,
            PartTimeEmployeeId = getPTEmp.Id,
            ShiftId = dto.ShiftId
        };

        _repository.Insert(model);
    }

    public void Update(WorkScheduleUpdateDTO dto)
    {
        var getUser = _userRepository.Get(dto.UserEmail);
        Employee emp = getUser.Employees.Where(emp => emp.WorkEmail == dto.UserEmail).FirstOrDefault();

        var model = _repository.GetById(dto.Id);
        model.IsApproved = dto.IsApproved;
        model.ManagerId = emp.Id;

        _repository.Update(model);
    }
}
