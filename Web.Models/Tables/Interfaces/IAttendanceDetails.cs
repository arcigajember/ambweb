using System;

namespace Web.Models.Tables.Interfaces
{
    public interface IAttendanceDetails
    {
        int AttendanceDetailsId { get; set; }
        int AttendanceHeaderId { get; set; }
        int TimeTypeId { get; set; }
        DateTime Date { get; set; }
        DateTime Time { get; set; }
        TimeType TimeType { get; set; }
    }
}
