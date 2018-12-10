namespace TelegramBotNet
{
    public static class Texts
    {
        public static readonly string Greeting = "Вас приветствует помощник АСИ \"Молодые профессионалы\"! Вот что я умею: \r\n" +
                                                 "- Показать расписание на сегодня \r\n" +
                                                 "- Запланировать командировку \r\n" +
                                                 "- Обратиться в IT-поддержку \r\n" +
                                                 "Что бы Вы хотели сделать?";

    }

    public static class ContextCommands
    {
        public static readonly string Schedule = "/todaySchedule";

        public static readonly string Trip = "/manageTrip";

        public static readonly string ItSupport = "/itSupport";

        public static readonly string Clear = "/clear";
    }

    public static class TripTexts
    {
        public static readonly string NewTrip = "Новая командировка";

        public static readonly string EditDestination = "Место назначения";

        public static readonly string EditOrganization = "Организация";

        public static readonly string EditStartDate = "Дата начала";

        public static readonly string EditEndDate = "Дата окончания";

        public static readonly string Back = "Назад";

        public static readonly string EditTrip = "Редактировать";

        public static readonly string Finish = "Подтвердить";

        public static readonly string ClearData = "Очистить данные";

        public static readonly string Cancel = "Отменить";

        public static readonly string Skip = "Пропустить";

        public static readonly string SuccessCreated = "- Спасибо! Ваша командировка направлена на оформление в отдел кадров. " +
                                                       "Я пришлю вам на электронную почту детальную информацию и проинформирую о статусе оформления. " +
                                                       "Приятной поездки.";
    }

    public static class TripCommands
    {
        public static readonly string NewTrip = "/newTrip";

        public static readonly string EditDestination = "/editDestination";

        public static readonly string EditOrganization = "/editOrganization";

        public static readonly string EditStartDate = "/editStartDate";

        public static readonly string EditEndDate = "/editEndDate";

        public static readonly string Back = "/back";

        public static readonly string EditTrip = "/editTrip";

        public static readonly string Finish = "/confirm";

        public static readonly string ClearData = "/clear";

        public static readonly string Cancel = "/cancel";

        public static readonly string Skip = "/skip";
    }
}