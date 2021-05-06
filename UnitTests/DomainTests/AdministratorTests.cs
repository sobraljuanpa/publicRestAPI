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
            Administrator administrator = new Administrator();

            Assert.IsNotNull(administrator);
        }

        [TestMethod]
        public void SetAndGetEmailTest()
        {
            string email = "juan@hotmail.com";

            Administrator administrator = new Administrator();

            administrator.Email = email;

            Assert.AreEqual(email, administrator.Email);
        }

        [TestMethod]
        public void SetAndGetNameTest()
        {
            string name = "Juan";

            Administrator administrator = new Administrator();

            administrator.Name = name;

            Assert.AreEqual(name, administrator.Name);
        }

        [TestMethod]
        public void SetAndGetPasswordTest()
        {
            string password = "123456";

            Administrator administrator = new Administrator();

            administrator.Password = password;

            Assert.AreEqual(password, administrator.Password);
        }

        [TestMethod]
        public void SetAndGetTokenTest()
        {
            string token = "unToken";

            Administrator administrator = new Administrator();

            administrator.Token = token;

            Assert.AreEqual(token, administrator.Token);
        }
    }
}
