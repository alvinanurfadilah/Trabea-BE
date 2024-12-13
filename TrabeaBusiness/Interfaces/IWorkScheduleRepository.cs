using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Interfaces;

public interface IWorkScheduleRepository
{
    List<WorkSchedule> Get(DateTime workDate);
    List<WorkSchedule> Get(int weekNumber);
    List<WorkSchedule> Get(string name, int shiftId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize);
    WorkSchedule GetById(int id);
    void Insert(WorkSchedule workSchedule);
    void Update(WorkSchedule workSchedule);

    int Count(string name, int shiftId, DateTime startDate, DateTime endDate);
}
