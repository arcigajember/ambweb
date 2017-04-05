using System;

namespace Web.Models.Tables.Interfaces
{
    public interface IAuditModel
    {
        Guid AuditId { get; set; }
        string UserName { get; set; }
        string IPAddress { get; set; }
        string AreaAccessed { get; set; }
        DateTime Timeaccessed { get; set; }
        string Parameters { get; set; }
    }
}
