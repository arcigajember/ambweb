using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models.Tables;

namespace Web.Models.ModelView
{
    public class SectionView
    {
        public Section Section { get; set; }
        public IEnumerable<Subject> Subject { get; set; }
        public List<SelectListItem> RoomOptions { get; set; }

        public SectionView(IEnumerable<Room> room)
        {
            RoomOptions = new List<SelectListItem>();

            foreach (var set in room)
            {
                RoomOptions.Add(new SelectListItem
                {
                    Text = string.Format("{0} - {1}", set.RoomNumber, set.RoomName),
                    Value = set.RoomId.ToString()
                });
            }
        }
    }
}
