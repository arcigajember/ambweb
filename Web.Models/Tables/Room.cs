using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class Room : IRoom
    {
        public int RoomId { get; set; }

        [Required, Display(Name ="Room Number")]
        public string RoomNumber { get; set; }

        [Required, Display(Name ="Room Name")]
        public string RoomName { get; set; }

        [ReadOnly(true)]
        public bool IsActive { get; set; }

        public bool IsSelected { get; set; }

        public string RoomDetails => string.Format("{0} / {1}", RoomNumber, RoomName);
    }
}
