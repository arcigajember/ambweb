using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Tables;

namespace Web.DataLayer.Interface
{
    public interface IAuditLogRepository
    {
        IEnumerable<AuditModel> SelectAll();
        AuditModel SelectById(Guid id);
        void Insert(AuditModel model);
    };
}
