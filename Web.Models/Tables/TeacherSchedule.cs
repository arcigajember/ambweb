using System;
using System.ComponentModel.DataAnnotations;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class TeacherSchedule : ITeacherSchedule
    {
        public int TeacherScheduleId { get; set; }
        public int SectionSubjectId { get; set; }
        public int TeacherId { get; set; }
        public int WeekDayId { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

    }
}
