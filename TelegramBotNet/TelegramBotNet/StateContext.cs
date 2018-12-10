using System.Collections.Concurrent;

namespace TelegramBotNet
{
    public static class StateContext
    {
        private static ConcurrentDictionary<long, UserContext> _contexts = new ConcurrentDictionary<long, UserContext>();

        public static void Set(long chatId, UserContext context)
        {
            _contexts[chatId] = context;
        }

        public static UserContext Get(long chatId)
        {
            UserContext value = UserContext.Undefined;
            return _contexts.GetOrAdd(chatId, value);
        }

        public static void Clear(long chatId)
        {
            _contexts[chatId] = UserContext.Undefined;
        }

        public static void TrySetByMessage(long chatId, string command)
        {
            if (command.Equals(ContextCommands.Schedule))
            {
                Set(chatId, UserContext.Schedule);
            }
            else if (command.Equals(ContextCommands.Trip))
            {
                Set(chatId, UserContext.Trip);
            }
            else if (command.Equals(ContextCommands.ItSupport))
            {
                Set(chatId, UserContext.ITSupport);
            }
        }
    }

    public enum UserContext
    {
        Undefined,
        Trip,
        Schedule,
        ITSupport
    }
}