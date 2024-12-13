using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Repositories;

public class WorkScheduleRepository : IWorkScheduleRepository
{
    private readonly TrabeaContext _context;

    public WorkScheduleRepository(TrabeaContext context)
    {
        _context = context;
    }

    public List<WorkSchedule> Get(DateTime workDate)
    {
        return _context.WorkSchedules
            .Include(ws => ws.PartTimeEmployee)
            .Include(ws => ws.Shift)
            .Where(ws => ws.IsApproved == true && ws.WorkDate == workDate)
            .ToList();
    }

    public List<WorkSchedule> Get(int weekNumber)
    {
        return _context.WorkSchedules
            .Include(ws => ws.PartTimeEmployee)
            .Include(ws => ws.Shift)
            .Where(ws => ws.IsApproved == true)
            .GroupBy(ws => ws.WorkDate)
            .Select(ws => ws.First())
            .ToList();
    }

    public List<WorkSchedule> Get(string name, int shiftId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize)
    {
        return _context.WorkSchedules
            .Include(ws => ws.PartTimeEmployee)
            .Include(ws => ws.Shift)
            .Where(ws => (ws.PartTimeEmployee.FirstName + ws.PartTimeEmployee.LastName).ToLower().Contains(name ?? "".ToLower()) && (ws.ShiftId == shiftId || 0 == shiftId) && ws.WorkDate >= startDate && ws.WorkDate <= endDate && ws.IsApproved == null)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public WorkSchedule GetById(int id)
    {
        return _context.WorkSchedules.Find(id) ?? throw new NullReferenceException();
    }

    public void Insert(WorkSchedule workSchedule)
    {
        _context.WorkSchedules.Add(workSchedule);
        _context.SaveChanges();
    }

    public void Update(WorkSchedule workSchedule)
    {
        _context.WorkSchedules.Update(workSchedule);
        _context.SaveChanges();
    }

    public int Count(string name, int shiftId, DateTime startDate, DateTime endDate)
    {
        return _context.WorkSchedules
             .Include(ws => ws.PartTimeEmployee)
             .Include(ws => ws.Shift)
             .Where(ws => (ws.PartTimeEmployee.FirstName + ws.PartTimeEmployee.LastName).ToLower().Contains(name ?? "".ToLower()) && (ws.ShiftId == shiftId || 0 == shiftId) && ws.WorkDate >= startDate && ws.WorkDate <= endDate && ws.IsApproved == null)
              .Count();
    }
}
