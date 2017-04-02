namespace Web.Models.Tables.Interfaces
{
    public interface ITimeType
    {
        int TimeTypeId { get; set; }
        string TimeTypeName { get; set; }
        bool IsActive { get; set; }
    }
}
