using Telegram.Bot.Types;
using TelegramBotNet.Commands.Trip;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class EditingTripState : TripState
    {
        public EditingTripState(TripModel model)
            : base(model)
        {
            StateKind = TripStateKind.EditingTrip;
        }

        public override bool HandleMessage(IState state, Message message)
        {
            var text = message.Text;
            var chatId = message.Chat.Id;
            if (TryHandleInnerState(state, message))
            {
                this.SendInvitation(message);
                return true;
            }
            if (text.Equals(TripCommands.EditDestination))
            {
                _innerState = new ChangingDestinationState(_model);
            }
            else if (text.Equals(TripCommands.EditOrganization))
            {
                _innerState = new ChangingOrganizationState(_model);
            }
            else if (text.Equals(TripCommands.EditStartDate))
            {
                _innerState = new ChangingStartDate(_model);
            }
            else if (text.Equals(TripCommands.EditEndDate))
            {
                _innerState = new ChangingEndDate(_model);
            }
            else if (text.Equals(TripCommands.Finish))
            {
                if (!TripHandlers.FinishCreation(chatId, _model))
                {
                    BotClient.Instance.Bot.SendTextMessageAsync(chatId, "Проверьте доступность сервера командировок и попробуйте еще раз", replyMarkup: TripMarkups.EditingTripMarkup);
                    return false;
                }
            }
            else
            {
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, "Команда не распознана попробуйте еще раз", replyMarkup: TripMarkups.EditingTripMarkup);
                return false;
            }
            _innerState.SendInvitation(message);
            return true;
        }

        private bool TryHandleInnerState(IState state, Message message)
        {
            bool result = false;
            if (_innerState != null)
            {
                result = _innerState.HandleMessage(state, message);
            }

            return result;
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
                                                 "Выберите поле для редактирования или отправьте на рассмотрение");
            }
        }
    }
}