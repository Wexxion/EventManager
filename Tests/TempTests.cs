using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.DomainLibs;
using TaskManager.AppLayer;

namespace Tests
{

    [TestClass]
    public class TempTests
    {
        [TestMethod]
        public void ReflectionTest()
        {
            var pattern_list = new List<string> {"one", "two", "three"};
            var testObject = new CommandArgumentPattern(pattern_list);
            Assert.AreEqual(testObject, new CommandArgumentPattern(pattern_list));
        }
    }
}
