using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.DataLayer.Interface
{
    public interface IAttendanceSectionRepository : IDisposable
    {
        Task<IEnumerable<TimeType>> AttendanceTimeType(int? timeTypeId);
        Task<IEnumerable<Student>> AttendanceToday();
        Task<IEnumerable<AttendanceDetails>> AttendanceDetails(int? studentId);
        Task<IEnumerable<Student>> AttendanceSection(int? sectionId);
        Task<IEnumerable<AttendanceDetails>> AttendanceStudent(int? studentId, DateTime? dateFrom, DateTime? dateTo);
        Task<Student> AttendanceReportStudent(int? studentId, DateTime? dateFrom, DateTime? dateTo);
        Task<IEnumerable<AttendanceLog>> AttendanceLog(MessageLogView modelView);
        Task<IEnumerable<SmsDetails>> SmsLog(MessageLogView modelView);
    }
}
