using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.DomainLibs;
using TaskManager.AppLayer;

namespace Tests
{

    class SpecialCommand : BaseCommand
    {
        [Pattern("[listed: one, two, three] [any]")]
        public int HandleSpecialLexem(List<string> args)
        {
            return 1;
        }

        public void NoAttributedMethod(List<string> args)
        {
            
        }
    }

    [TestClass]
    public class TempTests
    {
        [TestMethod]
        public void ReflectionTest()
        {
            var pattern_list = new List<string> {"two", "one", "three"};
            var testObject = new CommandArgumentPattern(pattern_list);
            Assert.AreEqual(testObject, new CommandArgumentPattern(new List<string> {"one", "two", "three"}));
        }

        [TestMethod]
        public void AttributedMethodsTest()
        {
            var pattern = new CommandPattern("[listed: two, one, three] [any]");
            var testingInstance = new SpecialCommand();
            Assert.AreEqual(testingInstance.MethodsDict.Count, 1);
            Assert.IsTrue(testingInstance.MethodsDict.ContainsKey(pattern));
            Assert.AreEqual(
                testingInstance.MethodsDict[pattern].Invoke(testingInstance, 
                new object[]
                {
                    new List<string> {"unusable text now"}
                }), 1);
        }
    }
}
