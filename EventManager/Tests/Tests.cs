using System;
using System.Linq;
using AppLayer;
using DomainLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoLayer.MessengerInterfaces;
using TaskManager;

namespace Tests
{
    [TestClass]
    public class AppLayersTests
    {
        [TestMethod]
        public void TestReminder()
        {
            var eventStorage = new MockEventStorage();
            var testEvent = eventStorage.GetAll().ElementAt(0);
            //Start at 12:00, remind in 1 hour
            testEvent.SetStaringTime(new DateTime(2000, 1, 1, 12, 0, 0));
            testEvent.SetFirstReminder(new TimeSpan(1, 0, 0));

            var remindSuccess = false;
            var reminder = new Reminder(1000 * 10, eventStorage);
            reminder.OnRemind += response => remindSuccess = true;

            //Don't remind at 10:00
            reminder.Remind(() => new DateTime(2000, 1, 1, 10, 0, 0));
            Assert.IsFalse(remindSuccess);
            remindSuccess = false;

            //Remind at 11:00 because remind time is 1 hour => 12-1=11
            reminder.Remind(() => new DateTime(2000, 1, 1, 11, 0, 0));
            Assert.IsTrue(remindSuccess);
            remindSuccess = false;

            //Don't remind at 12:00
            reminder.Remind(() => new DateTime(2000, 1, 1, 12, 0, 0));
            Assert.IsFalse(remindSuccess);
        }
    }

    [TestClass]
    public class SessionsTests
    {
        private SessionHandler GetSessionHandler()
        {
            var path = Environment.CurrentDirectory;
            var commandLoader = new CommandLoader(path + "\\..\\..\\..\\bin\\Debug\\Plugins",
                new MockEventStorage(), new MockPersonsStorage());
            return new SessionHandler(commandLoader.GetCommands());
        }

        private void TestSession(SessionHandler handler, Person person, string requestText, 
            string expectedText, ResponseStatus expectedStatus)
        {
            var request = new BaseRequest(person, requestText);
            var response = handler.ProcessMessage(request);
            Assert.AreEqual(expectedText, response.Text);
            Assert.AreEqual(expectedStatus, response.Status);
        }

        [TestMethod]
        public void TestLogin()
        {
            var handler = GetSessionHandler();

            //Person in Database
            var person1 = new Person(1, "John", "Smith", "JohnSmith");
            TestSession(handler, person1, 
                requestText: "Login",
                expectedText:"You've already logged in", 
                expectedStatus:ResponseStatus.Abort);

            //Person not in Database
            var person2 = new Person(5, "Bob", "Kibob", "1337");
            TestSession(handler, person2,
                requestText: "Login",
                expectedText: "You've successfully logged in",
                expectedStatus: ResponseStatus.Abort);
        }

        [TestMethod]
        public void TestList()
        {
            var handler = GetSessionHandler();

            //Person not in Database
            var person = new Person(5, "Bob", "Kibob", "1337");
            TestSession(handler, person,
                requestText: "All users",
                expectedText: "1) JohnSmith\n\r2) ADC\n\r3) SuperBob\n\r",
                expectedStatus: ResponseStatus.Abort);

            //Adding person to Database
            TestSession(handler, person,
                requestText: "Login",
                expectedText: "You've successfully logged in",
                expectedStatus: ResponseStatus.Abort);

            //Person in Database
            TestSession(handler, person,
                requestText: "All users",
                expectedText: "1) JohnSmith\n\r2) ADC\n\r3) SuperBob\n\r4) 1337\n\r",
                expectedStatus: ResponseStatus.Abort);
        }

        [TestMethod]
        public void TestEventList()
        {
            var handler = GetSessionHandler();

            //Person in Database
            var person1 = new Person(1, "John", "Smith", "JohnSmith");
            TestSession(handler, person1,
                requestText: "My events",
                expectedText: "1) Test1 /more0\r\n",
                expectedStatus: ResponseStatus.Expect);

            //Get more info about event
            TestSession(handler, person1,
                requestText: "/more0",
                expectedText: "Event Name : Test1\r\nDescription : Description1\r\n",
                expectedStatus: ResponseStatus.Expect);

            //Close session
            TestSession(handler, person1,
                requestText: "back",
                expectedText: "Ok",
                expectedStatus: ResponseStatus.Abort);

            //Person not in Database
            var person2 = new Person(5, "Bob", "Kibob", "1337");
            TestSession(handler, person2,
                requestText: "My events",
                expectedText: "You don't have any events",
                expectedStatus: ResponseStatus.Abort);
        }

        [TestMethod]
        public void TestDeleteEvents()
        {
            var handler = GetSessionHandler();

            //Person in Database
            var person = new Person(1, "John", "Smith", "JohnSmith");
            TestSession(handler, person,
                requestText: "Delete all events",
                expectedText: "All your events have been deleted",
                expectedStatus: ResponseStatus.Abort);

            //Check all user's events
            TestSession(handler, person,
                requestText: "My events",
                expectedText: "You don't have any events",
                expectedStatus: ResponseStatus.Abort);
        }

        [TestMethod]
        public void TestAdd()
        {
            var handler = GetSessionHandler();

            var person = new Person(5, "Bob", "Kibob", "1337");
            var expectedEvent = "Event Name : Test\r\n" +
                                "Description : Annoying testing... Sucks\r\n" +
                                "Start time : 01.01.2000 12:00:00\r\n" +
                                "End time : 01.01.2000 22:00:00\r\n";

            //Add event
            TestSession(handler, person,
                requestText: "Add event",
                expectedText: "Choose",
                expectedStatus: ResponseStatus.Expect);

            //Set Name
            TestSession(handler, person,
                requestText: "Name",
                expectedText: $"Set event name{Emoji.Pointer}",
                expectedStatus: ResponseStatus.Expect);

            TestSession(handler, person,
                requestText: "Test",
                expectedText: Emoji.CheckMark,
                expectedStatus: ResponseStatus.Expect);

            //Set Description
            TestSession(handler, person,
                requestText: "Description",
                expectedText: $"Set event description{Emoji.Pointer}",
                expectedStatus: ResponseStatus.Expect);

            TestSession(handler, person,
                requestText: "Annoying testing... Sucks",
                expectedText: Emoji.CheckMark,
                expectedStatus: ResponseStatus.Expect);

            //Set Start time
            TestSession(handler, person,
                requestText: "Start time",
                expectedText: "Select the appropriate time or set your\r\n" +
                              "Support format:\r\n" +
                              "hh:mm:ss\r\n" +
                              "dd.mm.yyyy\r\n" +
                              "dd.mm.yyyy hh:mm\r\n" +
                              "dd.mm.yyyy hh:mm:ss",
                expectedStatus: ResponseStatus.Expect);

            TestSession(handler, person,
                requestText: "01.01.2000 12:00:00",
                expectedText: Emoji.CheckMark,
                expectedStatus: ResponseStatus.Expect);

            //Set End time
            TestSession(handler, person,
                requestText: "End time",
                expectedText: "Select the appropriate time or set your\r\n" +
                              "Support format:\r\n" +
                              "hh:mm:ss\r\n" +
                              "dd.mm.yyyy\r\n" +
                              "dd.mm.yyyy hh:mm\r\n" +
                              "dd.mm.yyyy hh:mm:ss",
                expectedStatus: ResponseStatus.Expect);

            TestSession(handler, person,
                requestText: "01.01.2000 22:00:00",
                expectedText: Emoji.CheckMark,
                expectedStatus: ResponseStatus.Expect);

            //Don't forget to save
            TestSession(handler, person,
                requestText: "Save",
                expectedText: Emoji.CheckMark,
                expectedStatus: ResponseStatus.Abort);

            //Check what we've got
            TestSession(handler, person,
                requestText: "My events",
                expectedText: "1) Test /more0\r\n",
                expectedStatus: ResponseStatus.Expect);

            TestSession(handler, person,   
                requestText: "/more0",      //Get more info about event
                expectedText: expectedEvent,
                expectedStatus: ResponseStatus.Expect);

            TestSession(handler, person,
                requestText: "back",        //Close session
                expectedText: "Ok",
                expectedStatus: ResponseStatus.Abort);
        }
    }
}
