using System.Collections.Generic;

namespace Web.Models.Tables.Interfaces
{
    public interface ISection
    {
        int SectionId { get; set; }
        int? RoomId { get; set; }
        string SectionName { get; set; }
        bool IsActive { get; set; }
        IEnumerable<Subject> Subject { get; set; }
        Room Room { get; set; }
    }
}
