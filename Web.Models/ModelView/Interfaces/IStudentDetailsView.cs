using System.Collections.Generic;
using Web.Models.Tables;

namespace Web.Models.ModelView.Interfaces
{
    public interface IStudentDetailsView
    {
        Student Student { get; set; }
        IEnumerable<Guardian> Guardian { get; set; }
    }
}
