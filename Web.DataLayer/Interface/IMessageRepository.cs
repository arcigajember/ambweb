using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.DataLayer.Interface
{
    public interface IMessageRepository : IDisposable
    {
        Task<IEnumerable<GuardianContact>> GuardianContact(int? sectionId);
        Task<string> SendMessage(string phoneNumber, string message);
        Task<int> InsertSmsDetails(SmsDetails model);
        Task<int> StudentCheckTime(int? id);
        Task<int> AttendanceDetailsInsert(int? attendanceHeaderId, int? timeTypeId);
        Task<int> AttendanceHeaderInsert(int? studentId);
        Task AttendanceLogInsert(int? attendanceHeaderId, int? studentGuardianId, string apiResponse);
        Task<AttendanceDetails> SelectAttandanceDetailsId(int? attendanceDetailsId);
    }
}
