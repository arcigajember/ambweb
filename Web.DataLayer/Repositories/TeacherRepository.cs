using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Web.DataLayer.Interface;
using Web.DataLayer.Util;
using Web.Models.Tables;
using Web.Models.ModelView;

namespace Web.DataLayer.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DapperContext _dbContext;

        public TeacherRepository()
        {
            _dbContext = new DapperContext();
        }

        public async Task Delete(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TeacherId", id);

            await _dbContext.Connection.ExecuteAsync("TeacherDelete", p, commandType: CommandType.StoredProcedure);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> Insert(Teacher model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@FirstName", model.FirstName);
            p.Add("@LastName", model.LastName);
            p.Add("@MiddleName", model.MiddleName);
            p.Add("@Address", model.Address);
            p.Add("@IsActive", model.IsActive);
            p.Add("@TeacherId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("TeacherInsert", p, commandType: CommandType.StoredProcedure);

            return p.Get<int>("@TeacherId");
        }

        public async Task InsertSchedule(TeacherSaveSchedule modelView)
        {
            var selectedSubjectId = modelView.Subject.ToList()
                .Where(s => s.IsSelected)
                .Select(s => s.SubjectId);

            var subjectId = selectedSubjectId as IList<int> ?? selectedSubjectId.ToList();
            if (subjectId.IsAny())
            {
                foreach (var id in subjectId)
                {
                    DynamicParameters p = new DynamicParameters();
                    p.Add("@TeacherId", modelView.Teacher.TeacherId);
                    p.Add("@SectionId", modelView.SectionId);
                    p.Add("@SubjectId", id);

                    await _dbContext.Connection.ExecuteAsync("TeacherScheduleInsert", p, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public async Task<IEnumerable<Teacher>> SelectAll()
        {
            return await _dbContext.Connection.QueryAsync<Teacher>("TeacherSelectAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<Teacher> SelectById(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TeacherId", id);

            var result = await _dbContext.Connection.QueryAsync<Teacher>("TeacherSelectById", p, commandType: CommandType.StoredProcedure);

            return result.ToList().FirstOrDefault();
        }

        public async Task<IEnumerable<SubjectSectionView>> SelectDetailsById(int? teacherId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TeacherId", teacherId);

            var resultSubject = await _dbContext.Connection.QueryAsync<Subject>("TeacherSelectSubject", p,
                commandType: CommandType.StoredProcedure);

            var subjectLst = resultSubject as IList<Subject> ?? resultSubject.ToList();
            var modelView = new List<SubjectSectionView>();
            if (subjectLst.IsAny())
            {

                foreach (var item in subjectLst)
                {
                    SubjectSectionView model = new SubjectSectionView
                    {
                        Subject = item
                    };
                    p.Add("@SubjectId", item.SubjectId);

                    var resultSection = await _dbContext.Connection.QueryAsync<Section, Room, Section>("TeacherSelectSection",
                        (section, room) =>
                        {
                            section.Room = room;
                            return section;
                        }, p, commandType: CommandType.StoredProcedure,
                        splitOn: "RoomId");

                    var sectionLst = resultSection as IList<Section> ?? resultSection.ToList();
                    if (sectionLst.IsAny())
                    {
                        model.Section = sectionLst;
                        modelView.Add(model);
                    }
                }

            }

            return modelView;
        }

        public async Task Update(Teacher model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@TeacherId", model.TeacherId);
            p.Add("@FirstName", model.FirstName);
            p.Add("@LastName", model.LastName);
            p.Add("@MiddleName", model.MiddleName);
            p.Add("@Address", model.Address);

            await _dbContext.Connection.ExecuteAsync("TeacherUpdate", p, commandType: CommandType.StoredProcedure);
        }
    }
}
