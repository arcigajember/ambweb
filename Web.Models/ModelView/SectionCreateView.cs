using System.Collections.Generic;
using Web.Models.Tables;

namespace Web.Models.ModelView
{
    public class SectionCreateView
    {
        public Section Section { get; set; }
        public IEnumerable<Subject> Subject { get; set; }
       
    }
}
