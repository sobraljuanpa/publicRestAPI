using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTests
{
	[TestClass]
	public class ProblemTests
	{
		[TestMethod]
		public void EmptyConstructorTest()
        {
			Problem p = new Problem();

			Assert.IsNotNull(p);
        }

		[TestMethod]
		public void SetAndGetIdTest() 
		{
			Problem p = new Problem();

			p.Id = 1;

			Assert.AreEqual(1, p.Id);
		}

		[TestMethod]
		public void SetAndGetNameTest()
        {
			Problem p = new Problem();

			p.Name = "Depresión";

			Assert.AreEqual("Depresión", p.Name);
        }

		
	}
}
