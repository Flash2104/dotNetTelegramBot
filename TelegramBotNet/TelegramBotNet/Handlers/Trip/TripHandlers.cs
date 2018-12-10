using System;
using System.Globalization;
using Telegram.Bot.Types;
using TelegramBotNet.Models;

namespace TelegramBotNet.Commands.Trip
{
    public static class TripHandlers
    {
        public static bool TryEditDestination(Message message, TripModel model)
        {
            var success = false;
            var chatId = message.Chat.Id;

            var dest = message.Text;
            if (string.IsNullOrWhiteSpace(dest))
            {
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, "Пожалуйста введите место назначения");
            }
            else
            {
                model.Destination = dest;
                success = true;
            }
            return success;
        }

        public static bool TryEditOrganization(Message message, TripModel model)
        {
            var success = false;
            var chatId = message.Chat.Id;
            var organization = message.Text;
            if (string.IsNullOrWhiteSpace(organization))
            {
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, "Пожалуйста введите название организации");
            }
            else
            {
                success = true;
                model.Organization = organization;
            }
            return success;
        }

        public static bool EditStartDate(Message message, TripModel model)
        {
            var success = false;
            var chatId = message.Chat.Id;
            var dateText = message.Text;
            if (string.IsNullOrWhiteSpace(dateText))
            {
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, "Введите пожалуйста дату начала");
            }

            DateTime dateTime;
            if (!DateTime.TryParse(dateText, new CultureInfo("ru-Ru"), DateTimeStyles.None, out dateTime))
            {
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, "Не могу распознать формат даты, попробуйте снова");
            }
            else
            {
                success = true;
                model.StartDate = dateTime;
            }
            return success;
        }

        public static bool EditEndDate(Message message, TripModel model)
        {
            var success = false;
            var chatId = message.Chat.Id;
            var dateText = message.Text;
            if (string.IsNullOrWhiteSpace(dateText))
            {
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, "Введите пожалуйста дату окончания");
            }

            DateTime dateTime;
            if (!DateTime.TryParse(dateText, new CultureInfo("ru-Ru"), DateTimeStyles.None, out dateTime))
            {
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, "Не могу распознать формат даты, попробуйте снова");
            }
            else
            {
                model.EndDate = dateTime;
                success = true;
            }
            return success;
        }
        
        public static bool FinishCreation(long chatId, TripModel model)
        {
            bool result = false;
            var itemId = BotClient.Instance.PlatformClient.CreateTripRecord(model);

            var task = BotClient.Instance.ProcessServiceClient.CreateWithObjectId("pa.1", itemId);

            task.ContinueWith(task1 =>
            {
                var resp = task.Result;
                result = resp.Success;
                BotClient.Instance.Bot.SendTextMessageAsync(chatId, resp.Success ? TripTexts.SuccessCreated : "Произошла ошибка при оформлении.");
            });
            BotClient.Instance.ClearData(chatId);
            return result;
        }
    }
}