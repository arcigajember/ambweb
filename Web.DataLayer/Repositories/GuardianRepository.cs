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
    public class GuardianRepository : IGuardianRepository
    {
        private readonly DapperContext _dbContext;

        public GuardianRepository()
        {
            _dbContext = new DapperContext();
        }

        public async Task Delete(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@GuardianId", id);

            await _dbContext.Connection.ExecuteAsync("GuardianDelete", p, commandType: CommandType.StoredProcedure);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> Insert(Guardian model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@FirstName", model.FirstName);
            p.Add("@LastName", model.LastName);
            p.Add("@IsActive", model.IsActive);
            p.Add("@MiddleName", model.MiddleName);
            p.Add("@Street", model.Street);
            p.Add("@Barangay", model.Barangay);
            p.Add("@Municipality", model.Municipality);
            p.Add("@Province", model.Province);
            p.Add("@ContactNumber", model.ContactNumber);
            p.Add("@GuardianId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("GuardianInsert", p, commandType: CommandType.StoredProcedure);

            return p.Get<int>("@GuardianId");
        }

        public async Task<IEnumerable<Guardian>> SelectAll()
        {
            return await _dbContext.Connection.QueryAsync<Guardian>("GuardianSelectAll",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Guardian> SelectById(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@GuardianId", id);

            IEnumerable<Guardian> guardian = await _dbContext.Connection.QueryAsync<Guardian>("GuardianSelectById", p,
                commandType: CommandType.StoredProcedure);

            return guardian.ToList().FirstOrDefault();
        }

        public async Task Update(Guardian model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@FirstName", model.FirstName);
            p.Add("@LastName", model.LastName);
            p.Add("@MiddleName", model.MiddleName);
            p.Add("@Street", model.Street);
            p.Add("@Barangay", model.Barangay);
            p.Add("@Municipality", model.Municipality);
            p.Add("@Province", model.Province);
            p.Add("@ContactNumber", model.ContactNumber);
            p.Add("@GuardianId", model.GuardianId);


            await _dbContext.Connection.ExecuteAsync("GuardianUpdate", p,
                commandType: CommandType.StoredProcedure);
        }
    }
}
