using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly TrabeaContext _context;

    public RoleRepository(TrabeaContext context)
    {
        _context = context;
    }

    public List<Role> Get()
    {
        return _context.Roles.ToList();
    }

    public Role Get(int id)
    {
        return _context.Roles.Find(id) ?? throw new NullReferenceException();
    }
}
