using Web.Models.Tables.Interfaces;

namespace Web.Models.Tables
{
    public class AspNetUsers : IAspNetUsers
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}
