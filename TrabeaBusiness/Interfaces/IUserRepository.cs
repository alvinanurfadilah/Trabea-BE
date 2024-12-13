using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Interfaces;

public interface IUserRepository
{
    User Get(string email);
    void Insert(User user);
    void Update(User user);
}
