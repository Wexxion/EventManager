using System;
using AppLayer;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace TaskManager.AppLayer.Sessions.AddSession.Commands
{
    public class SetDateTimeCommand : SessionCommand
    {
        private Action<DateTime> SetAction { get;}
        public SetDateTimeCommand(string name, Action<DateTime> setAction)
            : base(name, new TextResponse("Select the appropriate time or set your\r\n" +
                                   "Support format:\r\n" +
                                   "hh:mm:ss\r\n" +
                                   "dd.mm.yyyy\r\n" +
                                   "dd.mm.yyyy hh:mm\r\n" +
                                   "dd.mm.yyyy hh:mm:ss",
                ResponseStatus.Expect))
        {
            SetAction = setAction;
            
        }

        public override IResponse Apply(IRequest message)
        {
            DateTime date;

            if (!DateTime.TryParse(message.Command, out date))
                return new TextResponse($"{Emoji.CrossMark} Parse exception", ResponseStatus.Close);
            SetAction(date);
            return new TextResponse(Emoji.CheckMark, ResponseStatus.Close);
        }
    }
}