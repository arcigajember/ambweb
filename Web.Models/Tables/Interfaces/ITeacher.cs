using System.Collections.Generic;

namespace Web.Models.Tables.Interfaces
{
    public interface ITeacher
    {
        int? TeacherId { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
        bool IsActive { get; set; }
    }
}
