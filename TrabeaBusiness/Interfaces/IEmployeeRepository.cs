using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Interfaces;

public interface IEmployeeRepository
{
    List<Employee> Get(int pageNumber, int pageSize, string fullName);
    Employee Get(int id);
    void Insert(Employee employee);
    void Update(Employee employee);
    int Count(string  fullName);
}
