using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.DataLayer.Repositories;

namespace Web.UnitTest
{
    [TestFixture]
    public class TeacherTest
    {
        [Test]
        public async Task TeacherSelectById()
        {
            var repo = new TeacherRepository();

            var result = await repo.SelectDetailsById(1);

            //Console.WriteLine("Teacher: {0} {1}", result.Teacher.FirstName, result.Teacher.LastName);

            //foreach (var subject in result.Subject)
            //{
            //    Console.WriteLine("Subject: {0}", subject.SubjectName);
            //    foreach (var section in result.Section)
            //    {
            //        Console.WriteLine("Section: {0} {1}",section.SectionName, section.Room.RoomName);
            //    }
            //}
        }
    }
}
