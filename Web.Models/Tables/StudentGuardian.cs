using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class StudentGuardian : IStudentGuardian
    {
        public int StudentGuardianId { get; set; }
        public int StudentId { get; set; }
        public int GuardianId { get; set; }
        public string Relation { get; set; }
    }
}
