using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab3;

namespace Lab3.Tests
{
    [TestClass]
    public class WorkRepositoryTests
    {
        [TestMethod]
        public void Add_IncreasesCount()
        {
            var repo = new WorkRepository();
            repo.Add(new LabWork("A", "B", DateTime.Now));
            Assert.AreEqual(1, repo.Count);
        }

        [TestMethod]
        public void Add_Null_DoesNotIncreaseCount()
        {
            var repo = new WorkRepository();
            repo.Add(null);
            Assert.AreEqual(0, repo.Count);
        }

        [TestMethod]
        public void Clear_ResetsCount()
        {
            var repo = new WorkRepository();
            repo.Add(new LabWork("A", "B", DateTime.Now));
            repo.Clear();
            Assert.AreEqual(0, repo.Count);
        }
    }
}
