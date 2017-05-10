using System;
using System.Collections.Generic;
using TaskManager.RepoLayer;


namespace TaskManager.DomainLayer
{
    public class VEvent
    {
        public int Id { get; }
        public Person Creator { get; }
        public string Name { get; }
        public DateTime Start { private set; get; }
        public DateTime End { private set; get; }

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

        public string Description { private set; get; }
        public Location Location { get; }
        public TimeSpan FirstReminder { get; private set; }
        public TimeSpan SecondReminder { get; private set; }
        public HashSet<Person> Participants { get; }

        public VEvent(Person creator, string name)
        {
            this.Creator = creator;
            this.Name = name;
            this.Participants.Add(Creator);
        }

        public void SetDescription(string description) => this.Description = description;
        public void SetStaringTime(DateTime starting)
        {
            this.Start = starting;
            this.FirstReminder = new TimeSpan(0, 1, 0, 0);
            this.SecondReminder = new TimeSpan(0, 0, 20, 0);
        }

        public void SetEndingTime(DateTime ending)
        {
            if (ending < this.Start)
                throw new ArgumentException("Ending time should be more than starting time");
            this.End = ending;
        }

        public void AddParticipant(Person person)
            => this.Participants.Add(person);

        public void SetFirstReminder(TimeSpan span) => this.FirstReminder = span;
        public void SetSecondReminder(TimeSpan span) => this.SecondReminder = span;

    }
}
