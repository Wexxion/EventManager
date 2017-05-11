using Telegram.Bot.Types;

namespace TaskManager.RepoLayer.Messages
{
    public interface IMessageHandler
    {
        IResponsable ProcessMessage(Message message);
    }
}