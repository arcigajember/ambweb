using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Web.DataLayer.Interface;
using Web.DataLayer.Util;
using Web.Models.Tables;

namespace Web.DataLayer.Repositories
{
    public class SubjectRepository : IDefaultRepository<Subject>
    {

        private readonly DapperContext _dbContext;

        public SubjectRepository()
        {
            _dbContext = new DapperContext();
        }

        public async Task Delete(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SubjectId", id);

            await _dbContext.Connection.ExecuteAsync("SubjectDelete", p, commandType: CommandType.StoredProcedure);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> Insert(Subject model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SubjectName", model.SubjectName);
            p.Add("@Description", model.Description);
            p.Add("@IsActive", model.IsActive);
            p.Add("@SubjectId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("SubjectInsert", p,
                commandType: CommandType.StoredProcedure);

            return p.Get<int>("@SubjectId");
        }

        public async Task<IEnumerable<Subject>> SelectAll()
        {
            return await _dbContext.Connection.QueryAsync<Subject>("SubjectSelectAll",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Subject> SelectById(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SubjectId", id);

            IEnumerable<Subject> result = await _dbContext.Connection.QueryAsync<Subject>("SubjectSelectById",
                p, commandType: CommandType.StoredProcedure);

            return result.ToList().FirstOrDefault();
        }

        public async Task Update(Subject model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SubjectId", model.SubjectId);
            p.Add("@SubjectName", model.SubjectName);
            p.Add("@Description", model.Description);

            await _dbContext.Connection.ExecuteAsync("SubjectUpdate", p,
                commandType: CommandType.StoredProcedure);
        }
    }
}
