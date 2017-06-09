using System;
using System.Linq;
using System.Text;
using System.Threading;
using DomainLayer;
using RepoLayer;
using RepoLayer.MessengerInterfaces;

namespace AppLayer
{
    public class Reminder
    {
        public event Action<IResponse> OnRemind;
        private Timer Timer { get;}
        private IRepository<VEvent> EventStorage { get; }
        private readonly int timeInterval;
        private TimeSpan dTime;
        public Reminder(int timeInterval, IRepository<VEvent> eventStorage)
        {
            this.timeInterval = timeInterval;
            dTime = TimeSpan.FromMilliseconds(timeInterval);
            EventStorage = eventStorage;
            Timer = new Timer(state => Remind(() => DateTime.Now));
        }

        public void Start()
        {
            Timer.Change(0, timeInterval);
        }

        private VEvent[] GetEventsToRemind(Func<DateTime> getCurrentTime)
        {
            return EventStorage
                .GetAll()
                .Where(x => x.Start > getCurrentTime()) //Отсеиваем уже завершенные
                .Where(x =>
                {
                    var preStartTime = x.Start - getCurrentTime();
                    if (preStartTime - x.FirstReminder <= dTime && preStartTime - x.FirstReminder >= TimeSpan.Zero)
                        return true;
                    return x.SecondReminder - preStartTime <= dTime && x.SecondReminder - preStartTime >= TimeSpan.Zero;
                }).ToArray();
        }

        public void Remind(Func<DateTime> getCurrentTime)
        {
            foreach (var @event in GetEventsToRemind(getCurrentTime))
            {
                var person = @event.Creator;
                var result = new StringBuilder();
                result.Append("You asked me to remind you about this event: \r\n");
                result.Append(@event);
                var response = new RemindResponse(result.ToString(), person.TelegramId);
                OnRemind?.Invoke(response);
            }

        }
    }
}
