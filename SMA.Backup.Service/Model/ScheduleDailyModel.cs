using System;

namespace SMA.Backup.Service.Model
{
    public class ScheduleDailyModel
    {
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
