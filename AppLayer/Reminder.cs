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
        private Timer Timer { get;}
        private IRepository<VEvent> EventStorage { get; }
        public event Action<IResponse> OnRemind;
        private readonly TimeSpan dTime;
        public Reminder(int timeInterval, IRepository<VEvent> eventStorage)
        {
            dTime = TimeSpan.FromMilliseconds(timeInterval);
            EventStorage = eventStorage;
            Timer = new Timer(state => Remind());
            Timer.Change(0, timeInterval);
        }

        public void Remind()
        {
            var events = EventStorage
                .GetAll()
                .Where(x => x.Start > DateTime.Now) //Отсеиваем уже завершенные
                .Where(x =>
                {
                    var preStartTime = x.Start - DateTime.Now;
                    if (preStartTime - x.FirstReminder <= dTime && preStartTime - x.FirstReminder >= TimeSpan.Zero)
                        return true;
                    return x.SecondReminder - preStartTime <= dTime && x.SecondReminder - preStartTime >= TimeSpan.Zero;
                }).ToArray();
            foreach (var @event in events)
            {
                var person = @event.Creator;
                var result = new StringBuilder();
                result.Append($"You asked me to remind you about this event: \r\n");
                result.Append(@event);
                var response = new RemindResponse(result.ToString(), person.TelegramId);
                OnRemind?.Invoke(response);
            }

        }
    }
}
