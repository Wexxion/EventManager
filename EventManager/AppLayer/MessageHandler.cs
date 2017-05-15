using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.AppLayer.Commands;
using TaskManager.RepoLayer.Command;
using TaskManager.RepoLayer.MessengerInterfaces;

namespace TaskManager.AppLayer
{
    public class MessageHandler
    {
        private Dictionary<string, BaseCommand> EventCommandDict { get; }
        public MessageHandler(BaseCommand[] commans)
        {
            EventCommandDict = commans.ToDictionary(x => x.Name, x => x);
        }

        private MessageHandler()
        {
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
