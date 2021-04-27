using NUnit.Framework;
using Calendario.Core.Subjects;


namespace Calendario.UnitTests.Subjects
{
    public class User
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var user = new User(){};
            Assert.Pass();
        }
    }
}