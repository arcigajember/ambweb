using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Web.DataLayer.Interface;
using Web.DataLayer.Util;
using Web.Models.Tables;

namespace Web.DataLayer.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DapperContext _dbContext;

        public SectionRepository()
        {
            _dbContext = new DapperContext();
        }

        public async Task Delete(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionId", id);

            await _dbContext.Connection.ExecuteAsync("SectionDelete", p,
                commandType: CommandType.StoredProcedure);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> Insert(Section model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionName", model.SectionName);
            p.Add("@SectionId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("SectionInsert", p,
                commandType: CommandType.StoredProcedure);

            return p.Get<int>("@SectionId");
        }

        public async Task SectionRoomInsert(int? sectionId, int? roomId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionId", sectionId);
            p.Add("@roomId", roomId);

            await _dbContext.Connection.ExecuteAsync("SectionRoomInsert", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task SectionSubjectInsert(int? sectionId, int? subjectId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionId", sectionId);
            p.Add("@SubjectId", subjectId);

            await _dbContext.Connection.ExecuteAsync("SectionSubjectInsert", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Section> SectionSubjectSelectById(int? sectionId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionId", sectionId);

            using (var multi = await _dbContext.Connection.QueryMultipleAsync("SectionSubjectSelectById", p, commandType: CommandType.StoredProcedure))
            {
                List<Subject> subjectLst = new List<Subject>();

                Section section = multi.ReadAsync<Section>().Result.Single();

                //counter variable, get all the subject related to this section
                IEnumerable<Subject> ctr = multi.ReadAsync<Subject>().Result.ToList();
                //get all the value
                IEnumerable<Subject> subjectLstAll = await _dbContext.Connection.QueryAsync<Subject>("SubjectSelectAll", commandType: CommandType.StoredProcedure);


                if (ctr.IsAny())
                {
                    //put all the value from ctr to subjectLst and assign true to IsSelected property
                    foreach (var item in ctr)
                    {
                        subjectLst.Add(new Subject
                        {
                            Description = item.Description,
                            IsSelected = true,
                            SubjectId = item.SubjectId,
                            SubjectName = item.SubjectName
                        });
                    }
                }

                //exclude duplicate values
                section.Subject = subjectLst.Union(subjectLstAll)
                    .Distinct(new ComparerSubject());

                return section;
            }
        }

        public async Task<IEnumerable<Section>> SelectAll()
        {
            return await _dbContext.Connection.QueryAsync<Section>("SectionSelectall",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Section>> SelectAllWithRoom()
        {
            return await _dbContext.Connection.QueryAsync<Section, Room, Section>("SectionRoomSelectAll",
                (section, room) =>
                {
                    section.Room = room;
                    return section;
                }, commandType: CommandType.StoredProcedure,
                splitOn: "RoomId");
        }

        public async Task<Section> SelectById(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionId", id);

            IEnumerable<Section> result = await _dbContext.Connection.QueryAsync<Section>("SectionSelectById",
                p, commandType: CommandType.StoredProcedure);

            return result.ToList().FirstOrDefault();
        }

        public async Task Update(Section model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@SectionId", model.SectionId);
            p.Add("@SectionName", model.SectionName);

            await _dbContext.Connection.ExecuteAsync("SectionUpdate", p,
                commandType: CommandType.StoredProcedure);
        }
    }
}
