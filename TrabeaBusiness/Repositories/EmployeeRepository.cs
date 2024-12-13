using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly TrabeaContext _context;

    public EmployeeRepository(TrabeaContext context)
    {
        _context = context;
    }

    public List<Employee> Get(int pageNumber, int pageSize, string fullName)
    {
        return _context.Employees.Include(emp => emp.WorkEmailNavigation).Where(emp => (emp.FirstName + emp.LastName).ToLower().Contains(fullName ?? "".ToLower())).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }

    public Employee Get(int id)
    {
        return _context.Employees.Find(id) ?? throw new NullReferenceException();
    }

    public void Insert(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void Update(Employee employee)
    {
        _context.Employees.Update(employee);
        _context.SaveChanges();
    }

    public int Count(string fullName)
    {
        return _context.Employees.Include(emp => emp.WorkEmailNavigation).Where(emp => (emp.FirstName + emp.LastName).ToLower().Contains(fullName ?? "".ToLower())).Count();
    }
}
