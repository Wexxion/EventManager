using System;
using System.Collections.Generic;
using RepoLayer;

namespace DomainLayer
{
    public class VEvent : Entity
    {
        public VEvent() {}

        public Person Creator { get; private set; }
        [ToStringEntity("Event Name")]
        public string Name { get; private set; }
        [ToStringEntity]
        public string Description { private set; get; }
        [ToStringEntity("Start time")]
        public DateTime Start { private set; get; }
        [ToStringEntity("End time")]
        public DateTime End { private set; get; }
        public Location Location { get; private set; }
        public TimeSpan FirstReminder { get; private set; }
        public TimeSpan SecondReminder { get; private set; }
        public HashSet<Person> Participants { get; }
        public TimeSpan Length
        {
            get
            {
                if (End == null)
                    throw new ArgumentException("End doesn't exists!");
                if (Start == null)
                    throw new ArgumentException("Start doesn't exists!");
                return End - Start;
            }
        }

        public VEvent(Person creator, string name)
        {
            Creator = creator;
            Name = name;
            Participants = new HashSet<Person> {Creator};
        }

        public void SetName(string name) => Name = name;
        public void SetCreator(Person creator) => Creator = creator;
        public void SetDescription(string description) => Description = description;
        public void SetLocation(Location location) => Location = location;
        public void SetFirstReminder(TimeSpan span) => FirstReminder = span;
        public void SetSecondReminder(TimeSpan span) => SecondReminder = span;
        public void AddParticipant(Person person) => Participants.Add(person);
        public void SetStaringTime(DateTime starting)
        {
            Start = starting;
            FirstReminder = new TimeSpan(0, 1, 0, 0);
            SecondReminder = new TimeSpan(0, 0, 20, 0);
        }

        public void SetEndingTime(DateTime ending)
        {
            if (ending < Start)
                throw new ArgumentException("Ending time should be more than starting time");
            End = ending;
        }
    }
}