using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTests
{
    [TestClass]
    public class PsychologistTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            Psychologist ps = new Psychologist();

            Assert.IsNotNull(ps);
        }

        [TestMethod]
        public void SetAndGetIdTest()
        {
            Psychologist ps = new Psychologist();

            ps.Id = 1;

            Assert.AreEqual(1, ps.Id);
        }

        [TestMethod]
        public void SetAndGetPsychologistNameTest()
        {
            Psychologist ps = new Psychologist();

            ps.PsychologistName = "Florencia";

            Assert.AreEqual("Florencia", ps.PsychologistName);
        }

        [TestMethod]
        public void SetAndGetPsychologistSurnameTest()
        {
            Psychologist ps = new Psychologist();

            ps.PsychologistSurname = "Lopez";

            Assert.AreEqual("Lopez", ps.PsychologistSurname);
        }

        [TestMethod]
        public void SetAndGetIsRemoteTest()
        {
            Psychologist ps = new Psychologist();

            ps.IsRemote = true;

            Assert.IsTrue(ps.IsRemote);
        }

        [TestMethod]
        public void SetAndGetAddressTest()
        {
            Psychologist ps = new Psychologist();

            ps.Address = "Bulevar General Artigas 1199";

            Assert.AreEqual("Bulevar General Artigas 1199", ps.Address);
        }

        [TestMethod]
        public void SetExpertiseTest()
        {
            Psychologist ps = new Psychologist();
            ps.Expertise = new System.Collections.Generic.List<Problem>();
            Problem p = new Problem();

            ps.Expertise.Add(p);

            Assert.AreEqual(ps.Expertise.Count, 1);
        }
    }
}
