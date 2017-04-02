using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Web.DataLayer.Repositories;
using Web.Models.ModelView;

namespace Web.UnitTest
{
    [TestFixture]
    public class AttendanceLog
    {
        [Test]
        public async Task Test()
        {
            var repo = new AttendanceSectionRepository();
            var model = new MessageLogView
            {
                DateFrom = Convert.ToDateTime("2016-10-31 20:39:55.733"),
                DateTo = Convert.ToDateTime("2016-10-31 20:40:19.187")
            };
            var result = await repo.AttendanceLog(model);
        } 
    }
}
