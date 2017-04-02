using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.DataLayer.Interface
{
    public interface ITeacherRepository : IDefaultRepository<Teacher>
    {
        Task<IEnumerable<SubjectSectionView>> SelectDetailsById(int? teacherId);
        Task InsertSchedule(TeacherSaveSchedule model);
    }
}
