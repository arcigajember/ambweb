using System;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class AuditModel : IAuditModel
    {
        public Guid AuditId { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string AreaAccessed { get; set; }
        public DateTime Timeaccessed { get; set; }
        public string Parameters { get; set; }
    }
}
