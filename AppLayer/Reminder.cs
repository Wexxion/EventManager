using System;
using System.CodeDom;
using System.Linq;
using System.Threading;
using RepoLayer;
using TaskManager.DomainLayer;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace AppLayer
{
    public class Reminder
    {
        private Timer Timer { get;}
        private IRepository<VEvent> EventStorage { get; }
        public event Action<IResponse> OnRemind;
        private TimeSpan dTime;
        public Reminder(int timeInterval, IRepository<VEvent> eventStorage)
        {
            dTime = TimeSpan.FromMilliseconds(timeInterval);
            EventStorage = eventStorage;
            this.Timer = new Timer(state => Remind());
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

                var result = $"You asked me to remind you about this event: \r\n" +
                             $"Event name: {@event.Name}\r\n" +
                             $"Event description: {@event.Description}\r\n" +
                             $"Start time: {@event.Start}\r\n" +
                             $"End time: {@event.End}\r\n";

                var response = new RemindResponse(result, person.TelegramId);
                OnRemind?.Invoke(response);
            }

        }
    }
}
