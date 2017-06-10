using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DomainLayer;
using Newtonsoft.Json;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace AppLayer
{
    public class SessionHandler
    {

        private Dictionary<string, BaseBotSession> EventCommands { get; }
        private Dictionary<long,BaseBotSession> UsersActiveSessions { get; }
        private Dictionary<long, Dictionary<string, BaseBotSession>> UsersCommand { get; }
        private IEnumerable<BaseBotSession> Sessions { get; }
        private ICommandLoader CommandLoader { get; }
        public SessionHandler(ICommandLoader commandLoader)
        {
            CommandLoader = commandLoader;
            Sessions = CommandLoader.GetCommands();
            EventCommands = Sessions.ToDictionary(x => x.Name, x => x);
            UsersActiveSessions = new Dictionary<long, BaseBotSession>();
            UsersCommand = new Dictionary<long, Dictionary<string, BaseBotSession>>();
        }

        public Dictionary<string, BaseBotSession> GetSessionCommand()
        {
            return CommandLoader.GetCommands().ToDictionary(x => x.Name, x => x);
        }

        public IResponse ProcessMessage(IRequest message)
        {
            var command = message.Command;
            var personId = ((Person)message.Author).TelegramId;
            if (!UsersActiveSessions.ContainsKey(personId) || UsersActiveSessions[personId] == null)
            {
                if (EventCommands.ContainsKey(command))
                {
                    if (!UsersActiveSessions.ContainsKey(personId))
                    {
                        UsersCommand.Add(personId, GetSessionCommand());
                        UsersActiveSessions.Add(personId, UsersCommand[personId][command]);
                    }
                    else UsersActiveSessions[personId] = UsersCommand[personId][command];
                }
                else
                    return new ButtonResponse(
                        "No such command implemented!",
                        EventCommands.Keys.ToArray(), ResponseStatus.Abort);
            }
            try
            {
                var response = UsersActiveSessions[personId].Execute(message);
                if (response.Status == ResponseStatus.Expect)
                    return response;
                UsersActiveSessions[personId] = null;
                return new ButtonResponse(response.Text, EventCommands.Keys.ToArray(), ResponseStatus.Abort);
            }
            catch (ArgumentException)
            {
                return new ButtonResponse(
                    "Incorrect command arguments!",
                    EventCommands.Keys.ToArray(), ResponseStatus.Abort);
            }
        }
    }
}