
using Telegram.Bot.Types;
using TelegramBotNet.Commands;

namespace TelegramBotNet.States
{
    public interface IState
    {
        bool HandleMessage(IState state, Message message);
        void SendInvitation(Message message);
    }
}