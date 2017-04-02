namespace Web.Models.Tables.Interfaces
{
    public interface IStudentGuardian
    {
        int StudentGuardianId { get; set; }
        int StudentId { get; set; }
        int GuardianId { get; set; }
        string Relation { get; set; }
    }
}
