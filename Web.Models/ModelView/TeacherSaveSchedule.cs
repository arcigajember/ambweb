using System.Collections.Generic;
using Web.Models.Tables;

namespace Web.Models.ModelView
{
    public class TeacherSaveSchedule
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Subject> Subject { get; set; }
        public int SectionId { get; set; }
    }
}
