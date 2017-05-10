using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.DomainLayer;
using TaskManager.AppLayer;
using TaskManager.RepoLayer;

namespace Tests
{
    class SpecialCommand : BaseCommand
    {
        public SpecialCommand() : base("special") {}

        [Pattern("[listed: one, two, three] [any]")]
        public CommandResponse HandleSpecialLexem(Message message)
        {
            if (message.Args.Count == 0)
                return new CommandResponse("1");
            return new CommandResponse(string.Join("+", message.Args));
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
            var response = (CommandResponse)testingInstance
                .MethodsDict[pattern]
                .Invoke(testingInstance, new object[] {new Message("")});
            Assert.AreEqual(response.Text, new CommandResponse("1").Text);
        }

        [TestMethod]
        public void CommandSuccessfullExecutionTest()
        {
            var command = new SpecialCommand();
            Assert.AreEqual(
                command.Execute(new Message("three something")).Text,
                new CommandResponse("three+something").Text);
        }

        [TestMethod]
        public void CommandUnsuccessfullExecutionTest()
        {
            var command = new SpecialCommand();
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new Message("four three one")));
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new Message("")));
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new Message("four smth")));
        }
    }
}
