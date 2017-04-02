using System;
using System.ComponentModel.DataAnnotations;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class AttendanceDetails : IAttendanceDetails
    {
        public int AttendanceDetailsId { get; set; }
        public int AttendanceHeaderId { get; set; }
        public int TimeTypeId { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time), DisplayFormat(DataFormatString = "{0:h:mm:ss tt}")]
        public DateTime Time { get; set; }
        public virtual TimeType TimeType { get; set; }
    }
}
