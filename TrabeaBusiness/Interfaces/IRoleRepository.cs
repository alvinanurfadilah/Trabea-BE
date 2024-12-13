using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Interfaces;

public interface IRoleRepository
{
    List<Role> Get();
    Role Get(int id);
}
