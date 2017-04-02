namespace Web.Models.Tables.Interfaces
{
    public interface IGuardian
    {
        int GuardianId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string Street { get; set; }
        string Barangay { get; set; }
        string Municipality { get; set; }
        string Province { get; set; }
        string ContactNumber { get; set; }
        bool IsActive { get; set; }
    }
}
