using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.Tables;

namespace Web.DataLayer.Interface
{
    public interface ISectionRepository : IDefaultRepository<Section>
    {
        Task SectionSubjectInsert(int? sectionId, int? subjectId);
        Task SectionRoomInsert(int? sectionId, int? roomId);
        Task<Section> SectionSubjectSelectById(int? sectionId);
        Task<IEnumerable<Section>> SelectAllWithRoom();
    }
}
