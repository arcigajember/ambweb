using System.Collections.Generic;
using Web.Models.Tables;

namespace Web.Models.ModelView
{
    public class TeacherScheduleView
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<SubjectSectionView> SubjectSectionView { get; set; }
    }
}
