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
            Category category = new Category();

            Assert.IsNotNull(category);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Category category  = new Category();

            category.Id = 1;

            Assert.AreEqual(1, category.Id);
        }

        [TestMethod]
        public void SetAndGetNameTest()
        {
            string name = "Deportes";

            Category category  = new Category();

            category.Name = name;

            Assert.AreEqual(name, category.Name);
        }
    }
}
