using System;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class SmsDetails : ISmsDetails
    {
        public int SmsDetailsId { get; set; }
        public string SmsType { get; set; }
        public int StudentGuardianId { get; set; }
        public string AspNetUserId { get; set; }
        public DateTime DateSent { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Student Student { get; set; }
        public virtual Guardian Guardian { get; set; }
    }
}
