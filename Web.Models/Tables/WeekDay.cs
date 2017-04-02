using System.Collections.Generic;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class WeekDay : IWeekDay
    {
        public int WeekDayId { get; set; }
        public string WeekDayName { get; set; }

        public virtual IEnumerable<TeacherSchedule> TeacherSectionSubject { get; set; }
    }
}
