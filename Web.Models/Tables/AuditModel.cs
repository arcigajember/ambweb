using System;
using System.ComponentModel.DataAnnotations;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class AuditModel : IAuditModel
    {
        public Guid AuditId { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Display(Name = "Area Accessed")]
        public string AreaAccessed { get; set; }

        [Display(Name = "Time Accessed")]
        public DateTime Timeaccessed { get; set; }
        public string Parameters { get; set; }
    }
}
