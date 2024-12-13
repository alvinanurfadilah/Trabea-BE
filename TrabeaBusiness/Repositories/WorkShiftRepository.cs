using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Repositories;

public class WorkShiftRepository : IWorkShiftRepository
{
    private readonly TrabeaContext _context;

    public WorkShiftRepository(TrabeaContext context)
    {
        _context = context;
    }

    public List<WorkShift> Get()
    {
        return _context.WorkShifts.ToList();
    }
}
