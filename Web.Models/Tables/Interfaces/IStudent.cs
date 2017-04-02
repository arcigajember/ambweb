namespace Web.Models.Tables.Interfaces
{
    public interface IStudent
    {
        int StudentId { get; set; }
        int SectionId { get; set; }
        string StudentNumber { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string MiddleName { get; set; }
        string Street { get; set; }
        string Barangay { get; set; }
        string Municipality { get; set; }
        string Province { get; set; }
        string ContactNumber { get; set; }
        string Status { get; set; }
        string Gender { get; set; }
        bool IsActive { get; set; }
        string Uid { get; set; }

        Room Room { get; set; }
        Section Section { get; set; }
    }
}
