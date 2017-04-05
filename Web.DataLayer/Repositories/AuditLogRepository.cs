using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Web.DataLayer.Interface;
using Web.DataLayer.Util;
using Web.Models.Tables;

namespace Web.DataLayer.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly DapperContext _dbContext;

        public AuditLogRepository()
        {
            _dbContext = new DapperContext();
        }

        public void Insert(AuditModel model)
        {
            var p = new DynamicParameters();
            p.Add("@AuditId", model.AuditId);
            p.Add("@UserName", model.UserName);
            p.Add("@IPAddress", model.IPAddress);
            p.Add("@AreaAccessed", model.AreaAccessed);
            p.Add("@Timeaccessed", model.Timeaccessed);
            p.Add("@Parameters", model.Parameters);

            _dbContext.Connection.Execute("AuditLogInsert", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<AuditModel>> SelectAll()
        {
            return await _dbContext.Connection.QueryAsync<AuditModel>("AuditLogSelect",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<AuditModel> SelectById(Guid? id)
        {
            var p = new DynamicParameters();
            p.Add("@AuditId", id);

            IEnumerable<AuditModel> result = await  _dbContext.Connection.QueryAsync<AuditModel>("AuditLogSelectById", p,
                commandType: CommandType.StoredProcedure);

            return result.ToList().FirstOrDefault();
        }
    }
}
