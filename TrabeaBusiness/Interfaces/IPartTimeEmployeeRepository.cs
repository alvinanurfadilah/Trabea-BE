using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Interfaces;

public interface IPartTimeEmployeeRepository
{
    List<PartTimeEmployee> Get(int pageNumber, int pageSize, string fullName);
    PartTimeEmployee Get(int id);
    void Insert(PartTimeEmployee entity);
    void Update(PartTimeEmployee entity);
    int Count(string fullName);
}
