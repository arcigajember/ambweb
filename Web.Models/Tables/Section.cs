using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class Section : ISection
    {

        public int SectionId { get; set; }

        public int? RoomId { get; set; }

        [Required, Display(Name = "Section name")]
        public string SectionName { get; set; }

        [ReadOnly(true)]
        public bool IsActive { get; set; }

        public virtual IEnumerable<Subject> Subject { get; set; }
        public virtual Room Room { get; set; }

    }
}
