using System.Collections.Generic;
using Web.Models.Tables;

namespace Web.Models.ModelView
{
    public class SubjectSectionView
    {
        public Subject Subject { get; set; }
        public IEnumerable<Section> Section { get; set; }
    }
}
