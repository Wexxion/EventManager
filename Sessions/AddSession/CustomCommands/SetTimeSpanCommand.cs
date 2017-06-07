using System;
using System.Collections.Generic;
using System.Linq;
using AppLayer;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace AddSession.CustomCommands
{
    public class SetTimeSpanCommand : SessionCommand
    {
        private static readonly Dictionary<string, TimeSpan> SupportDate = new Dictionary<string, TimeSpan>()
        {
            {"half an hour", new TimeSpan(0, 0, 30, 0)},
            {"1 hour", new TimeSpan(0, 1, 0, 0)},
            {"2 hour", new TimeSpan(0, 2, 0, 0)},

        };
        private Action<TimeSpan> TimeAction { get; }
        public SetTimeSpanCommand(string name,Action<TimeSpan> timeAction)
            : base(
                name,
                new ButtonResponse("Select the appropriate time or set your\r\n" +
                                   "Support format:\r\n" +
                                   "dd.hh:mm:ss\r\nhh:mm:ss",
                    SupportDate.Select(x => x.Key).ToArray(), ResponseStatus.Expect))
        {
            TimeAction = timeAction;
        }

        public override IResponse Apply(IRequest message)
        {
            TimeSpan date;
            var command = message.Command;
            if (SupportDate.ContainsKey(command))
                date = SupportDate[command];
            else if (!TimeSpan.TryParse(message.Command, out date))
                return new TextResponse($"{Emoji.CrossMark} Parse exception", ResponseStatus.Close);
            TimeAction(date);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Close);
        }
    }
}