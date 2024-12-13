using System;
using System.Collections.Generic;

namespace TrabeaDataAccess.Models
{
    public partial class WorkSchedule
    {
        public int Id { get; set; }
        public DateTime WorkDate { get; set; }
        public bool? IsApproved { get; set; }
        public int? ManagerId { get; set; }
        public int PartTimeEmployeeId { get; set; }
        public int ShiftId { get; set; }

        public virtual Employee? Manager { get; set; }
        public virtual PartTimeEmployee PartTimeEmployee { get; set; } = null!;
        public virtual WorkShift Shift { get; set; } = null!;
    }
}
