using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models.ModelView
{
    public class MessageLogView
    {
        public MessageLogView()
        {
            SearchOptions = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Attendance Log",
                    Value = "1"
                },
                new SelectListItem
                {
                    Text = "Event Log",
                    Value =  "2"
                }
            };
        }

        [Required]
        public int? SearchId { get; set; }
        
        public List<SelectListItem> SearchOptions { get; set; }

        [Required]
        [Display(Name = "From"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateFrom { get; set; }

        [Required]
        [Display(Name = "To"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateTo { get; set; }
    }
}
