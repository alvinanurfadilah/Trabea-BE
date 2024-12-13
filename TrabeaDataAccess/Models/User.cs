using System;
using System.Collections.Generic;

namespace TrabeaDataAccess.Models
{
    public partial class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
            PartTimeEmployees = new HashSet<PartTimeEmployee>();
            Roles = new HashSet<Role>();
        }

        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PartTimeEmployee> PartTimeEmployees { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
