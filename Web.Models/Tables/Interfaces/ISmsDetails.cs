using System;

namespace Web.Models.Tables.Interfaces
{
    public interface ISmsDetails
    {
        int SmsDetailsId { get; set; }
        string SmsType { get; set; }
        int StudentGuardianId { get; set; }
        string AspNetUserId { get; set; }
        DateTime DateSent { get; set; }
        string Status { get; set; }
        string Message { get; set; }
    }
}
