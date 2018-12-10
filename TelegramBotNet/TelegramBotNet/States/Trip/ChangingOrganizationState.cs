using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotNet.Commands.Trip;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class ChangingOrganizationState: TripState
    {
        public ChangingOrganizationState(TripModel model) 
            : base(model)
        {
            StateKind = TripStateKind.ChangingOrganization;
        }

        public override bool HandleMessage(IState state, Message message)
        {
            return TripHandlers.TryEditOrganization(message, _model);
        }
        
        public override void SendInvitation(Message message)
        {
            BotClient.Instance.Bot.SendTextMessageAsync(message.Chat.Id, "Введите пожалуйста дату окончания", replyMarkup: TripMarkups.SkipMarkup);
        }
    }
}