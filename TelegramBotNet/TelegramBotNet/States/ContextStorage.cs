using System.Collections.Concurrent;
using Telegram.Bot.Types;

namespace TelegramBotNet.States
{
    public static class ContextStorage
    {
        private static readonly ConcurrentDictionary<long, Trip> _tripStorage = new ConcurrentDictionary<long, Trip>();


        public static IContextObject GetOrAdd(UserContext context, long chatId)
        {
            switch (context)
            {
                case UserContext.Trip:
                {
                    var trip = new Trip();
                    trip = _tripStorage.GetOrAdd(chatId, trip);
                    return trip;
                }
                default:
                    return null;
            }
        }

        public static void Clear(long chatId)
        {
            _tripStorage[chatId] = new Trip();
        }
    }

    public interface IContextObject
    {
        void Request(Message message);
    }
}