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
    public class RoomRepository : IRoomRepository
    {
        private readonly DapperContext _dbContext;

        public RoomRepository()
        {
            _dbContext = new DapperContext();
        }
        public async Task Delete(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@RoomId", id);

            await _dbContext.Connection.ExecuteAsync("RoomDelete", p,
                commandType: CommandType.StoredProcedure);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> Insert(Room model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@RoomNumber", model.RoomNumber);
            p.Add("@RoomName", model.RoomName);
            p.Add("@IsActive", model.IsActive);
            p.Add("@RoomId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("RoomInsert", p,
                commandType: CommandType.StoredProcedure);

            return p.Get<int>("@RoomId");

        }

        public async Task<IEnumerable<Room>> SelectAll()
        {
            return await _dbContext.Connection.QueryAsync<Room>("RoomSelectAll",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Room> SelectById(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@RoomId", id);

            IEnumerable<Room> roomList = await _dbContext.Connection.QueryAsync<Room>("RoomSelectById", p,
                commandType: CommandType.StoredProcedure);

            return roomList.ToList().FirstOrDefault();
        }

        public async Task Update(Room model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@RoomId", model.RoomId);
            p.Add("@RoomNumber", model.RoomNumber);
            p.Add("@RoomName", model.RoomName);

            await _dbContext.Connection.ExecuteAsync("RoomUpdate", p,
                commandType: CommandType.StoredProcedure);
        }
    }
}
