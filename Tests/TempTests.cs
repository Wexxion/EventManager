using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.Messages;

namespace Tests
{
    public enum SpecialCommandFirstArg
    {
        One,
        Two,
        Three
    }
    public class SpecialCommand : BaseCommand
    {
        public SpecialCommand() : base("special")
        {
        }

        [Pattern(typeof(SpecialCommandFirstArg), typeof(string[]))]
        public Response HandleSpecialLexem(TgMessage message)
        {
            if (message.Args.Count == 0)
                return new Response("1");
            return new Response(string.Join("+", message.Args));
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
            var testObject = new ArgumentPattern(new List<string> { "two", "one", "three" });
            Assert.AreEqual(testObject, new ArgumentPattern(new List<string> {"one", "two", "three"}));
        }

        [TestMethod]
        public void ArgumentPatternsWithNullNotEqualToSmth()
        {
            var testObject = new ArgumentPattern(null);
            Assert.AreNotEqual(new ArgumentPattern(new List<string>()), testObject);
        }

        [TestMethod]
        public void AttributedMethodsAreInDict()
        {
            var pattern = new BaseCommandPattern(typeof(SpecialCommandFirstArg), typeof(string[]));
            var testingInstance = new SpecialCommand();
            Assert.IsTrue(testingInstance.MethodsDict.ContainsKey(pattern));
        }

        [TestMethod]
        public void AttributedMethodIsInvokable()
        {
            var pattern = new BaseCommandPattern(typeof(SpecialCommandFirstArg), typeof(string[]));
            var testingInstance = new SpecialCommand();
            var response = (Response)testingInstance
                .MethodsDict[pattern]
                .Invoke(testingInstance, new object[] { new TgMessage("") });
            Assert.AreEqual(response.Text, new Response("1").Text);
        }

        [TestMethod]
        public void CommandSuccessfullExecutionTest()
        {
            var command = new SpecialCommand();
            Assert.AreEqual(
                command.Execute(new TgMessage("/special three something")).Text,
                new Response("three+something").Text);
        }

        [TestMethod]
        public void CommandUnsuccessfullExecutionTest()
        {
            var command = new SpecialCommand();
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new TgMessage("/special four three one")));
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new TgMessage("")));
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new TgMessage("/special four smth")));
        }
    }
}
