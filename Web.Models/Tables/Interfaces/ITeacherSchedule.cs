using System;

namespace Web.Models.Tables.Interfaces
{
    public interface ITeacherSchedule
    {
        int TeacherScheduleId { get; set; }
        int SectionSubjectId { get; set; }
        int TeacherId { get; set; }
        int WeekDayId { get; set; }
        DateTime Time { get; set; }
    }
}
