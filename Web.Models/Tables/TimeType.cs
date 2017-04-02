using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class TimeType : ITimeType
    {
        public int TimeTypeId { get; set; }
        public string TimeTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
