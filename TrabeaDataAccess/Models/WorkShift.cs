using System;
using System.Collections.Generic;

namespace TrabeaDataAccess.Models
{
    public partial class WorkShift
    {
        public WorkShift()
        {
            WorkSchedules = new HashSet<WorkSchedule>();
        }

        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
