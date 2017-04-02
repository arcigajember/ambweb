using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models.ModelView
{
    public class MessageView
    {
        [Required]
        public int[] SectionId { get; set; }
        public MultiSelectList SectionList { get; set; }
        [Required]
        public string TextMessage { get; set; }
    }
}
