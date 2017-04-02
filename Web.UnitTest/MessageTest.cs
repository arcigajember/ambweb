using System.Threading.Tasks;
using NUnit.Framework;
using Web.DataLayer.Repositories;

namespace Web.UnitTest
{
    [TestFixture]
    public class MessageTest
    {
        [Test]
        public async Task SendMessage()
        {
            var repo = new MessageRepository();
            var result = await repo.SendMessage("09304514664", "TEL OPEN ME ITANG PC KU ATIN KUNG KUNAN BILIS");
        }
    }
}
