using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Repositories;

public class PartTimeEmployeeRepository : IPartTimeEmployeeRepository
{
    private readonly TrabeaContext _context;

    public PartTimeEmployeeRepository(TrabeaContext context)
    {
        _context = context;
    }

    public List<PartTimeEmployee> Get(int pageNumber, int pageSize, string fullName)
    {
        return _context.PartTimeEmployees.Include(ptEmp => ptEmp.WorkEmailNavigation).Where(ptEmp => (ptEmp.FirstName + ptEmp.LastName).ToLower().Contains(fullName ?? "".ToLower())).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }

    public PartTimeEmployee Get(int id)
    {
        return _context.PartTimeEmployees.Find(id) ?? throw new NullReferenceException();
    }

    public void Insert(PartTimeEmployee entity)
    {
        _context.PartTimeEmployees.Add(entity);
        _context.SaveChanges();
    }

    public void Update(PartTimeEmployee entity)
    {
        _context.PartTimeEmployees.Update(entity);
        _context.SaveChanges();
    }

    public int Count(string fullName)
    {
        return _context.PartTimeEmployees.Include(ptEmp => ptEmp.WorkEmailNavigation).Where(ptEmp => (ptEmp.FirstName + ptEmp.LastName).ToLower().Contains(fullName ?? "".ToLower())).Count();
    }
}
