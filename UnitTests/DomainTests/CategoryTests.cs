using Microsoft.VisualStudio.TestTools.UnitTesting;

using Domain;

namespace UnitTests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            Category c = new Category();

            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Category c = new Category();
            
            c.Id = 1;

            Assert.AreEqual(1, c.Id);
        }

        [TestMethod]
        public void SetAndGetNameTest()
        {
            Category c = new Category();

            c.Name = "Deportes";

            Assert.AreEqual("Deportes", c.Name);
        }
    }
}
