using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class Subject : ISubject
    {
      
        public int SubjectId { get; set; }

        [Required, Display(Name = "Subject")]
        public string SubjectName { get; set; }

        [Required]
        public string Description { get; set; }

        [ReadOnly(true)]
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }

        public virtual IEnumerable<Section> Section { get; set; }
    }
}
