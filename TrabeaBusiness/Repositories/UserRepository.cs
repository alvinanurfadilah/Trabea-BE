using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabeaBusiness.Interfaces;
using TrabeaDataAccess.Models;

namespace TrabeaBusiness.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TrabeaContext _context;

    public UserRepository(TrabeaContext context)
    {
        _context = context;
    }
    
    public User Get(string email)
    {
        return _context.Users.Include(u => u.Employees).Include(u => u.PartTimeEmployees).Include(u => u.Roles).Where(u => u.Email == email).FirstOrDefault() ?? throw new NullReferenceException();
    }

    public void Insert(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
