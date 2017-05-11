using TaskManager.RepoLayer.Command;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.RepoLayer.Messages
{
    public interface IMessageHandler
    {
        IResponsable ProcessMessage(Message message);
    }
}