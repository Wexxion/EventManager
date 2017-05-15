using System;
using System.Collections.Generic;
using TaskManager.AppLayer.Commands;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer
{
    public class MessageHandler
    {
        private Dictionary<string, BaseCommand> EventCommandDict { get; }
        public MessageHandler()
        {
            EventCommandDict = new Dictionary<string, BaseCommand>
            {
                {"event", new EventCommand()}
            };
        }
        public IResponse ProcessMessage(IRequest message)
        {
            if (!EventCommandDict.ContainsKey(message.Command))
                return new BaseResponse("No such command implemented!");
            try
            {
                return EventCommandDict[message.Command].Execute(message);
            }
            catch (ArgumentException)
            {
                return new BaseResponse("Incorrect command arguments!");
            }
        }
    }
}
