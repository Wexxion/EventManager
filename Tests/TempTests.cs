using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager.AppLayer;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessengerInterfaces;

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
        public IResponse HandleSpecialLexem(IRequest message)
        {
            if (message.Args.Count == 0)
                return new BaseResponse("1");
            return new BaseResponse(string.Join("+", message.Args));
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
            var response = (BaseResponse)testingInstance
                .MethodsDict[pattern]
                .Invoke(testingInstance, new object[] { new BaseRequest(null, ""),  });
            Assert.AreEqual(response.Text, new BaseResponse("1").Text);
        }

        [TestMethod]
        public void CommandSuccessfullExecutionTest()
        {
            var command = new SpecialCommand();
            Assert.AreEqual(
                command.Execute(new BaseRequest(null, "/special three something")).Text,
                new BaseResponse("three+something").Text);
        }

        [TestMethod]
        public void CommandUnsuccessfullExecutionTest()
        {
            var command = new SpecialCommand();
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new BaseRequest(null, "/special four three one")));
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new BaseRequest(null, "")));
            Assert.ThrowsException<ArgumentException>(
                () => command.Execute(new BaseRequest(null, "/special four smth")));
        }
    }
}
