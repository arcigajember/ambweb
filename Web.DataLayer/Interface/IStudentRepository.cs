using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.DataLayer.Interface
{
    public interface IStudentRepository : IDefaultRepository<Student>
    {
        Task<int> GetIdentity();
        Task InsertStudentGuardian(StudentView model);
        Task SectionInsert(StudentSectionView model);
        Task<StudentDetailsView> StudentDetails(int? id);
        Task<Section> StudentSection(int? id);
        Task StudentUpdateUid(int? id, string uid);
        Task<int> StudentCheckUid(string uid);
        Task<IEnumerable<Student>> StudentWithUid();
        Task<IEnumerable<Student>> StudentWithoutUid();
        Task<Student> StudentSelectByUid(string uid);
        Task<IEnumerable<GuardianContact>> StudentGuardianContact(int? studentId);
        Task StudentGuardianDelete(int? studentId, int? guardianId);

    }
}
