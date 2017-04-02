using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Web.Models.Tables;

namespace Web.Models.ModelView
{
    public class SectionReportView
    {
        public List<SelectListItem> SectionOptions { get; set; }

        [Required]
        public int SectionId { get; set; }

        [Required]
        [Display(Name="From"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }

        [Required]
        [Display(Name="To")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateTo { get; set; }

        public SectionReportView(IEnumerable<Section> sectionLst)
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
