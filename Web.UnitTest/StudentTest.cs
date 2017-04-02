using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Web.DataLayer.Repositories;

namespace Web.UnitTest
{
    [TestFixture]
    class StudentTest
    {
        [Test]
        public async Task SelectAll()
        {
            StudentRepository repo = new StudentRepository();

            var result = await repo.SelectAll();

            foreach (var item in result)
            {
                Console.WriteLine("Student Name:" + item.FirstName);
            }

        }

        [Test]
        public async Task SelectById()
        {
            StudentRepository repo = new StudentRepository();

            var result = await repo.SelectById(20);

            if (result != null)
            {
                Console.WriteLine("First Name: {0}", result.FirstName);
            }
        }

        [Test]
        public async Task GetIdentity()
        {
            StudentRepository repo = new StudentRepository();

            int id = await repo.GetIdentity();

            Assert.AreEqual(1, id);
        }
    }
}
