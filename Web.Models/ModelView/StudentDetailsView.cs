using System.Collections.Generic;
using Web.Models.ModelView.Interfaces;
using Web.Models.Tables;

namespace Web.Models.ModelView
{
    public class StudentDetailsView : IStudentDetailsView
    {
        public Student Student { get; set; }
        public IEnumerable<Guardian> Guardian { get; set; }
    }
}
