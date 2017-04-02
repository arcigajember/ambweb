using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.ModelView
{
    public class SectionSearch
    {
        [Required]
        public int? SectionId { get; set; }
        [Required]
        public DateTime? DateFrom { get; set; }
        [Required]
        public DateTime? DateTo { get; set; }

    }
}
