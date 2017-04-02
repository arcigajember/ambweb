namespace Web.Models.Tables.Interfaces
{
    public interface IRoom
    {
        int RoomId { get; set; }
        string RoomNumber { get; set; }
        string RoomName { get; set; }
        bool IsActive { get; set; }
    }
}
