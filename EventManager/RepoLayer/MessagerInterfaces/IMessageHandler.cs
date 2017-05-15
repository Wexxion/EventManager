using Telegram.Bot.Types;

namespace TaskManager.RepoLayer.MessagerInterfaces
{
    public interface IMessageHandler
    {
        IResponsable ProcessMessage(Message message);
    }
}