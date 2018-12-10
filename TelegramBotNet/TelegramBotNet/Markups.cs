using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotNet
{
    public static class Markups
    {
        private static InlineKeyboardButton ShowScheduleButton => new InlineKeyboardButton()
        {
            Text = "1",
            CallbackData = ContextCommands.Schedule
        };

        private static InlineKeyboardButton CreateTripButton => new InlineKeyboardButton()
        {
            Text = "2",
            CallbackData = ContextCommands.Trip

        };

        private static InlineKeyboardButton ItSupportButton => new InlineKeyboardButton()
        {
            Text = "3",
            CallbackData = ContextCommands.ItSupport
        };

        public static InlineKeyboardMarkup StartingMarkup => new InlineKeyboardMarkup(new[] { ShowScheduleButton, CreateTripButton, ItSupportButton });
    }

    public static class TripMarkups
    {



        private static KeyboardButton CancelButton => new KeyboardButton(TripCommands.Cancel);
        private static InlineKeyboardButton EditDestinationButton => new InlineKeyboardButton()
        {
            Text = "1",
            CallbackData = TripCommands.EditDestination
        };
        private static InlineKeyboardButton EditOrganizationButton => new InlineKeyboardButton()
        {
            Text = "2",
            CallbackData = TripCommands.EditOrganization
        };
        private static InlineKeyboardButton EditStartDateButton => new InlineKeyboardButton()
        {
            Text = "3",
            CallbackData = TripCommands.EditStartDate
        };
        private static InlineKeyboardButton EditEndDateButton => new InlineKeyboardButton()
        {
            Text = "4",
            CallbackData = TripCommands.EditEndDate
        };
        private static InlineKeyboardButton EditTripButton => new InlineKeyboardButton()
        {
            Text = TripTexts.EditTrip,
            CallbackData = TripCommands.EditTrip
        };
        private static KeyboardButton BackButton => new KeyboardButton(TripCommands.Back);
        private static InlineKeyboardButton SkipButton => new InlineKeyboardButton()
        {
            Text = TripTexts.Skip,
            CallbackData = TripCommands.Skip
        };
        private static InlineKeyboardButton FinishButton => new InlineKeyboardButton()
        {
            Text = TripTexts.Finish,
            CallbackData = TripCommands.Finish
        };

        public static InlineKeyboardMarkup SkipMarkup => new InlineKeyboardMarkup(new[] { SkipButton });

        public static InlineKeyboardMarkup EditingTripMarkup => new InlineKeyboardMarkup(DefaultEditingFieldList);

        public static InlineKeyboardMarkup FinishCreationTripMarkup => new InlineKeyboardMarkup(new[] { EditTripButton, FinishButton });

        private static List<InlineKeyboardButton> DefaultEditingFieldList => new List<InlineKeyboardButton>
        {
            EditDestinationButton,
            EditOrganizationButton,
            EditStartDateButton,
            EditEndDateButton,
            FinishButton
        };
    }
}