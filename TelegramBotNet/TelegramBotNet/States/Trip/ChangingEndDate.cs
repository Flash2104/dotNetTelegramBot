using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotNet.Commands.Trip;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class ChangingEndDate : TripState
    {
        public ChangingEndDate(TripModel model)
            : base(model)
        {
            StateKind = TripStateKind.ChangingEndDate;
        }

        public override bool HandleMessage(IState state, Message message)
        {
            return TripHandlers.EditEndDate(message, _model);
        }
        
        public override void SendInvitation(Message message)
        {
            BotClient.Instance.Bot.SendTextMessageAsync(message.Chat.Id, "Введите пожалуйста дату окончания", replyMarkup: TripMarkups.SkipMarkup);
        }
    }
}