using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotNet.Commands.Trip;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class ChangingDestinationState: TripState
    {
        public ChangingDestinationState(TripModel model) 
            : base(model)
        {
            StateKind = TripStateKind.ChangingDestination;
        }

        public override bool HandleMessage(IState state, Message message)
        {
            return TripHandlers.TryEditDestination(message, _model);
        }

        public override void SendInvitation(Message message)
        {
            BotClient.Instance.Bot.SendTextMessageAsync(message.Chat.Id, "Пожалуйста введите место назначения", replyMarkup: TripMarkups.SkipMarkup);
        }
    }
}