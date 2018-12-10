using Telegram.Bot.Types;
using TelegramBotNet.Commands.Trip;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class ChangingStartDate : TripState
    {
        public ChangingStartDate(TripModel model)
            : base(model)
        {
            StateKind = TripStateKind.ChangingStartDate;
        }

        public override bool HandleMessage(IState state, Message message)
        {
            return TripHandlers.EditStartDate(message, _model);
        }
        
        public override void SendInvitation(Message message)
        {
            BotClient.Instance.Bot.SendTextMessageAsync(message.Chat.Id, "Введите пожалуйста дату начала", replyMarkup: TripMarkups.SkipMarkup);
        }
    }
}