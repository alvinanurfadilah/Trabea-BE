using System;
using System.Collections.Generic;

namespace TrabeaDataAccess.Models
{
    public partial class Employee
    {
        public Employee()
        {
            WorkSchedules = new HashSet<WorkSchedule>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string WorkEmail { get; set; } = null!;

        public virtual User WorkEmailNavigation { get; set; } = null!;
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
