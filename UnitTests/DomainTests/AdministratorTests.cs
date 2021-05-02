using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTests
{
    [TestClass]
    public class AdministratorTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            Administrator a = new Administrator();

            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void SetAndGetEmailTest()
        {
            Administrator a = new Administrator();

            a.Email = "juan@hotmail.com";

            Assert.AreEqual("juan@hotmail.com", a.Email);
        }

        [TestMethod]
        public void SetAndGetNameTest()
        {
            Administrator a = new Administrator();

            a.Name = "Juan";

            Assert.AreEqual("Juan",a.Name);
        }

        [TestMethod]
        public void SetAndGetPasswordTest()
        {
            Administrator a = new Administrator();

            a.Password = "123456";

            Assert.AreEqual("123456", a.Password);
        }

        [TestMethod]
        public void SetAndGetTokenTest()
        {
            Administrator a = new Administrator();

            a.Token = "unToken";

            Assert.AreEqual("unToken", a.Token);
        }
    }
}
