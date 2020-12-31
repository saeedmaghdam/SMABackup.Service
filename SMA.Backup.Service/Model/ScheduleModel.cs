using System.Collections.Generic;

namespace SMA.Backup.Service.Model
{
    public class ScheduleModel
    {
        public ScheduleModel()
        {
            Interval = new List<ScheduleIntervalModel>();
            Daily = new List<ScheduleDailyModel>();
            Weekly = new List<ScheduleWeeklyModel>();
            Monthly = new List<ScheduleMonthlyModel>();
        }

        public ICollection<ScheduleIntervalModel> Interval
        {
            get;
            set;
        }

        public ICollection<ScheduleDailyModel> Daily
        {
            get;
            set;
        }

        public ICollection<ScheduleWeeklyModel> Weekly
        {
            get;
            set;
        }

        public ICollection<ScheduleMonthlyModel> Monthly
        {
            get;
            set;
        }
    }
}
