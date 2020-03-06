using System;
using System.CodeDom.Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kansū;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRepairs()
        {
            Assert.IsNotNull(Workshop.RepairsToday("406"));
        }

        [TestMethod]
        public void TestPartsMovement()
        {
            var results = PartsCage.EngineerParts();
            foreach (var row in results)
            {
                Console.WriteLine(row.PartDescription);
                Assert.IsNotNull(row.Engineer);
                Assert.IsNotNull(row.Location);
                Assert.IsNotNull(row.MovedAt);
                Assert.IsNotNull(row.PartDescription);
                Assert.IsNotNull(row.PartNumber);
                Assert.IsNotNull(row.Quantity);
            }
        }
    }
}
