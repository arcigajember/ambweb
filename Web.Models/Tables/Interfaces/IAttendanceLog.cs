namespace Web.Models.Tables.Interfaces
{
    public interface IAttendanceLog
    {
        int AttendanceLogId { get; set; }
        int AttendanceHeaderId { get; set; }
        int StudentGuardianId { get; set; }
        string ApiResponse { get; set; }
    }
}
