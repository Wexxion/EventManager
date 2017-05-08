using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.DomainLibs;
using TaskManager.AppLayer;

namespace Tests
{

    class SpecialCommand : BaseCommand
    {
        [Pattern("some pattern")]
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
            var pattern_list = new List<string> {"one", "two", "three"};
            var testObject = new CommandArgumentPattern(pattern_list);
            Assert.AreEqual(testObject, new CommandArgumentPattern(pattern_list));
        }

        [TestMethod]
        public void AttributedMethodsTest()
        {
            var pattern = new CommandArgumentPattern("some pattern");
            var testingInstance = new SpecialCommand();
            Assert.IsTrue(
                testingInstance.MethodsDict.ContainsKey(pattern)
            );

            Assert.AreEqual(
                testingInstance
                .MethodsDict[pattern]
                .Invoke(testingInstance, new object[] { new List<string> {"1"} }),
                1
                );
        }
    }
}
