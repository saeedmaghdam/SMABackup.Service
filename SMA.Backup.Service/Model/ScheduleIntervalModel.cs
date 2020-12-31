using System;

namespace SMA.Backup.Service.Model
{
    public class ScheduleIntervalModel
    {
        public long Minutes
        {
            get;
            set;
        }

        public DateTime LastExecution { 
            get; 
            set; 
        }
    }
}
