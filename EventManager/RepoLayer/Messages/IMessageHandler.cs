using TaskManager.RepoLayer.Command;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TaskManager.RepoLayer.Messages
{
    public interface IMessageHandler
    {
        void AnalyseMessage(Message message);
        void SendMessage(long id, IResponsable massageData, IReplyMarkup replyMarkup);
    }
}
