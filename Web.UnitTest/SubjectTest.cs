using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Web.DataLayer.Repositories;

namespace Web.UnitTest
{
    [TestFixture]
    public class SubjectTest
    {
        [Test]
        public async Task SectionSubjectSelectById()
        {
            SectionRepository repo = new SectionRepository();

            var result = await repo.SectionSubjectSelectById(5);

            foreach (var item in result.Subject)
            {
                Console.WriteLine(string.Format("SubjectName: {0} \nDescription:{1} \nSelected:{2}", item.SubjectName, item.Description, item.IsSelected));
            }
        }
    }
}
