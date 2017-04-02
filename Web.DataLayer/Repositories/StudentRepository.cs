using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Web.DataLayer.Interface;
using Web.DataLayer.Util;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.DataLayer.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DapperContext _dbContext;

        public StudentRepository()
        {
            _dbContext = new DapperContext();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<IEnumerable<Student>> SelectAll()
        {

            return await _dbContext.Connection.QueryAsync<Student>("StudentSelectAll", commandType: CommandType.StoredProcedure);
        }

        public async Task<Student> SelectById(int? id)
        {

            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", id);

            IEnumerable<Student> result = await _dbContext.Connection.QueryAsync<Student>("StudentSelectById", p, commandType: CommandType.StoredProcedure);

            return result.ToList().FirstOrDefault();
        }

        public async Task<int> Insert(Student model)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("@StudentNumber", model.StudentNumber);
            p.Add("@FirstName", model.FirstName);
            p.Add("@LastName", model.LastName);
            p.Add("@MiddleName", model.MiddleName);
            p.Add("@Street", model.Street);
            p.Add("@Barangay", model.Barangay);
            p.Add("@Municipality", model.Municipality);
            p.Add("@Province", model.Province);
            p.Add("@ContactNumber", model.ContactNumber);
            p.Add("@Status", model.Status);
            p.Add("@Gender", model.Gender);
            p.Add("@IsActive", model.IsActive);
            p.Add("@StudentId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("StudentInsert", p,
                commandType: CommandType.StoredProcedure);

            return p.Get<int>("@StudentId");
        }

        public async Task Delete(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", id);
            await _dbContext.Connection.ExecuteAsync("StudentDelete", p, commandType: CommandType.StoredProcedure);
        }

        public async Task Update(Student model)
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
            p.Add("@Status", model.Status);
            p.Add("@Gender", model.Gender);
            p.Add("@StudentId", model.StudentId);

            await _dbContext.Connection.ExecuteAsync("StudentUpdate", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> GetIdentity()
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dbContext.Connection.ExecuteAsync("StudentGetIdentity", p, commandType: CommandType.StoredProcedure);

            return p.Get<int>("@StudentId");
        }

        public async Task InsertStudentGuardian(StudentView model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", model.StudentId);
            p.Add("@GuardianId", model.GuardianId);
            p.Add("@Relation", model.Relation);

            await _dbContext.Connection.ExecuteAsync("StudentGuardianInsert", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task SectionInsert(StudentSectionView model)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", model.StudentId);
            p.Add("@SectionId", model.SectionId);

            await _dbContext.Connection.ExecuteAsync("StudentSectionUpdate", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<StudentDetailsView> StudentDetails(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", id);

            var result = await _dbContext.Connection.QueryAsync<Student, Section, Room, Student>("StudentDetailsById",
                (student, section, room) =>
                {
                    student.Section = section;
                    student.Room = room;
                    return student;
                }, p, commandType: CommandType.StoredProcedure,
                splitOn: "SectionId, RoomId");

            var guardianResult =
                await _dbContext.Connection.QueryAsync<Guardian, StudentGuardian, Guardian>("StudentGuardianById",
                    (guardian, studentGuardian) =>
                    {
                        guardian.StudentGuardian = studentGuardian;
                        return guardian;
                    }, p, commandType: CommandType.StoredProcedure,
                    splitOn: "StudentGuardianId");

            StudentDetailsView modelView = new StudentDetailsView
            {
                Student = result.FirstOrDefault(),
                Guardian = guardianResult
            };

            return modelView;
        }

        public async Task<Section> StudentSection(int? id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId");

            IEnumerable<Section> result = await _dbContext.Connection.QueryAsync<Section>("StudentSectionId", p,
                commandType: CommandType.StoredProcedure);

            return result.ToList().FirstOrDefault();
        }

        public async Task StudentUpdateUid(int? id, string uid)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", id);
            p.Add("@Uid", uid);

            await _dbContext.Connection.ExecuteAsync("StudentUpdateUid", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> StudentCheckUid(string uid)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Uid", uid);

            return await _dbContext.Connection.ExecuteScalarAsync<int>("StudentCheckUid", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Student>> StudentWithUid()
        {
            return
                await
                    _dbContext.Connection.QueryAsync<Student>("StudentWithUid",
                        commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Student>> StudentWithoutUid()
        {
            return
                await
                    _dbContext.Connection.QueryAsync<Student>("StudentWithoutUid",
                        commandType: CommandType.StoredProcedure);
        }

        public async Task<Student> StudentSelectByUid(string uid)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@Uid", uid);

            IEnumerable<Student> result =
                await _dbContext.Connection.QueryAsync<Student, Section, Student>("StudentSelectByUid",
                    (student, section) =>
                    {
                        student.Section = section;
                        return student;
                    }, p, commandType: CommandType.StoredProcedure,
                    splitOn: "SectionId");

            return result.ToList().FirstOrDefault();
        }

        public async Task<IEnumerable<GuardianContact>> StudentGuardianContact(int? studentId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", studentId);

            return await _dbContext.Connection.QueryAsync<GuardianContact>("StudentGuardianContact", p,
                commandType: CommandType.StoredProcedure);
        }

        public async Task StudentGuardianDelete(int? studentId, int? guardianId)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@StudentId", studentId);
            p.Add("@GuardianId", guardianId);

            await
                _dbContext.Connection.ExecuteAsync("StudentGuardianDelete", p, commandType: CommandType.StoredProcedure);
        }
    }
}
