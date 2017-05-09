using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.DomainLayer;
using TaskManager.AppLayer;

namespace Tests
{
    class SpecialCommand : BaseCommand
    {
        public SpecialCommand() : base("special") {}

        [Pattern("[listed: one, two, three] [any]")]
        public string HandleSpecialLexem(List<string> args)
        {
            if (args.Count == 0)
                return "1";
            return string.Join("+", args);
        }

        public void NoAttributedMethod(List<string> args)
        {
            
        }
    }

    [TestClass]
    public class BasicCommandsClassesTests
    {
        [TestMethod]
        public void ArgumentPatternsWithRandomArgumentOrderAreEqual()
        {
            var testObject = new CommandArgumentPattern(new List<string> { "two", "one", "three" });
            Assert.AreEqual(testObject, new CommandArgumentPattern(new List<string> {"one", "two", "three"}));
        }

        [TestMethod]
        public void ArgumentPatternsWithNullNotEqualToSmth()
        {
            var testObject = new CommandArgumentPattern(null);
            Assert.AreNotEqual(new CommandArgumentPattern(new List<string>()), testObject);
        }

        [TestMethod]
        public void AttributedMethodsAreInDict()
        {
            var pattern = new CommandPattern("[listed: two, one, three] [any]");
            var testingInstance = new SpecialCommand();
            Assert.IsTrue(testingInstance.MethodsDict.ContainsKey(pattern));
        }

        [TestMethod]
        public void AttributedMethodIsInvokable()
        {
            var pattern = new CommandPattern("[listed: two, one, three] [any]");
            var testingInstance = new SpecialCommand();
            Assert.AreEqual(
                testingInstance.MethodsDict[pattern].Invoke(testingInstance,
                new object[]
                {
                    new List<string>()
                }), "1");
        }

        [TestMethod]
        public void CommandSuccessfullExecutionTest()
        {
            var command = new SpecialCommand();
            Assert.AreEqual(
                command.Execute(new List<string> {"three", "something"}),
                "three+something");
        }

        [TestMethod]
        public void CommandUnsuccessfullExecutionTest()
        {
            var command = new SpecialCommand();
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new List<string> {"four", "three", "one"}));
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new List<string>()));
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new List<string> {"four", "smth"}));
        }
    }
}
