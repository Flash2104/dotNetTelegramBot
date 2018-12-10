using Telegram.Bot.Types;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class FinishCreationState : TripState
    {
        public FinishCreationState(TripModel model) 
            : base(model)
        {
            StateKind = TripStateKind.FinishCreation;
        }

        public override bool HandleMessage(IState state, Message message)
        {
            if (message.Text.Equals(TripCommands.EditTrip))
            {
                state = new EditingTripState(_model);
            }
            else if (message.Text.Equals(TripCommands.Finish))
            {

            }

            return true;
        }

        public override void SendInvitation(Message message)
        {
            bool hasValue = false;
            string destination = string.Empty;
            var chatId = message.Chat.Id;
            var tripInfo = _model;
            if (!string.IsNullOrEmpty(tripInfo.Destination))
            {
                destination = $"Место назначения: {tripInfo.Destination}.\r\n";
                hasValue = true;
            }
            string organization = string.Empty;
            if (!string.IsNullOrEmpty(tripInfo.Organization))
            {
                organization = $"Организация: {tripInfo.Organization}.\r\n";
                hasValue = true;
            }
            string startDate = string.Empty;
            if (tripInfo.StartDate.HasValue)
            {
                startDate = $"Дата начала: {tripInfo.StartDate.Value:dd-MM-yyyy}.\r\n";
                hasValue = true;
            }
            string endDate = string.Empty;
            if (tripInfo.EndDate.HasValue)
            {
                endDate = $"Дата окончания: {tripInfo.EndDate.Value:dd-MM-yyyy}.\r\n";
                hasValue = true;
            }

            if (hasValue)
            {
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, $"{destination}" +
                                                                    $"{organization}" +
                                                                    $"{startDate}" +
                                                                    $"{endDate}" +
                                                                    "Проверьте заполненные данные", replyMarkup: TripMarkups.FinishCreationTripMarkup);
            }
        }
    }
}