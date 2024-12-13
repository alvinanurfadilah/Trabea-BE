using System;
using System.Collections.Generic;

namespace TrabeaDataAccess.Models
{
    public partial class PartTimeEmployee
    {
        public PartTimeEmployee()
        {
            WorkSchedules = new HashSet<WorkSchedule>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalEmail { get; set; } = null!;
        public string PersonalPhoneNumber { get; set; } = null!;
        public string LastEducation { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? OnGoingEducation { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? ResignDate { get; set; }
        public string WorkEmail { get; set; } = null!;

        public virtual User WorkEmailNavigation { get; set; } = null!;
        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
