namespace Web.Models.Tables.Interfaces
{
    public interface ISubject
    {
        int SubjectId { get; set; }
        string SubjectName { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
    }
}
