using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class AttendanceHeader : IAttendanceHeader
    {
        public int AttendanceHeaderId { get; set; }
        public int StudentId { get; set; }
    }
}
