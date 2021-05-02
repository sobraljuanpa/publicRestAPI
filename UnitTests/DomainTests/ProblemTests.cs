using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Collections.Generic;

namespace UnitTests
{
	[TestClass]
	public class ProblemTests
	{
		[TestMethod]
		public void EmptyConstructorTest()
        {
			Problem problem = new Problem();

			Assert.IsNotNull(problem);
        }

		[TestMethod]
		public void SetAndGetIdTest() 
		{
			Problem problem = new Problem();

			problem.Id = 1;

			Assert.AreEqual(1, problem.Id);
		}

		[TestMethod]
		public void SetAndGetNameTest()
        {
			string name = "Depresión";

			Problem problem = new Problem();

			problem.Name = name;

			Assert.AreEqual(name, problem.Name);
        }

		[TestMethod]
		public void SetAndGetSpecialistTest()
        {
			Problem problem = new Problem();
			problem.Specialists = new System.Collections.Generic.List<Psychologist>();
			Psychologist psychologist = new Psychologist();

			problem.Specialists.Add(psychologist);
			int result = problem.Specialists.Count;

			Assert.AreEqual(result, 1);
		}

		
	}
}
