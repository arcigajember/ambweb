using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class AttendanceLog : IAttendanceLog
    {
        public int AttendanceLogId { get; set; }
        public int AttendanceHeaderId { get; set; }
        public int StudentGuardianId { get; set; }
        public string ApiResponse { get; set; }

        public virtual Student Student { get; set; }
        public virtual AttendanceDetails AttendanceDetails { get; set; }
        public virtual Guardian Guardian { get; set; }
    }
}
