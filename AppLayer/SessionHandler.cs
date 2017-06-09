using System;
using System.Collections.Generic;
using System.Linq;
using RepoLayer.MessengerInterfaces;
using RepoLayer.Session;

namespace AppLayer
{
    public class SessionHandler
    {
        private Dictionary<string, BaseBotSession> EventCommands { get; }
        private BaseBotSession ActiveSession { get; set; }
        public SessionHandler( IEnumerable<BaseBotSession> sessions )
        {
            EventCommands = sessions.ToDictionary(x => x.Name, x => x);
        }

        public IResponse ProcessMessage(IRequest message)
        {
            var command = message.Command;
            if (ActiveSession == null)
            {
                if (EventCommands.ContainsKey(command))
                    ActiveSession = EventCommands[command];
                else
                    return new ButtonResponse(
                        "No such command implemented!",
                        EventCommands.Keys.ToArray(), ResponseStatus.Abort);
            }
            try
            {
                var response = ActiveSession.Execute(message);
                if (response.Status == ResponseStatus.Expect)
                    return response;
                ActiveSession = null;
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