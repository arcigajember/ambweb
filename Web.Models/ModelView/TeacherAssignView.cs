using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.Tables;

namespace Web.Models.ModelView
{
    public class TeacherAssignView
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Subject> Subject { get; set; }
        public int SectionId { get; set; }
        public List<SelectListItem> SectionOptions { get; set; }


        public TeacherAssignView(IEnumerable<Section> sectionLst)
        {
            SectionOptions = new List<SelectListItem>();

            foreach (var section in sectionLst)
            {
                SectionOptions.Add(new SelectListItem
                {
                    Text = section.SectionName,
                    Value = section.SectionId.ToString()
                });
            } 
        }
    }
}
