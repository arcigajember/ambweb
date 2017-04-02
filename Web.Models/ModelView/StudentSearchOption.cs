using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models.ModelView
{
    public class StudentSearchOption
    {
        [Required]
        [Display(Name = "From"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }

        [Required]
        [Display(Name = "From"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateTo { get; set; }

        [Required]
        public int?[] StudentId { get; set; }

        public MultiSelectList StudentList { get; set; }
        
    }
}
