using System.Collections.Generic;

namespace Web.Models.Tables.Interfaces
{
    public interface IWeekDay
    {
        int WeekDayId { get; set; }
        string WeekDayName { get; set; }
        IEnumerable<TeacherSchedule> TeacherSectionSubject { get; set; }
    }
}
