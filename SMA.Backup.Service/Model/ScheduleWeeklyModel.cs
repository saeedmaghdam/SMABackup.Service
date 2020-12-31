using System;

namespace SMA.Backup.Service.Model
{
    public class ScheduleWeeklyModel
    {
        public DayOfWeek DayOfWeek
        {
            get;
            set;
        }

        public int Hour
        {
            get;
            set;
        }

        public int Minute
        {
            get;
            set;
        }

        public DateTime LastExecution
        {
            get;
            set;
        }
    }
}
