using System;
using System.Net;
using ASITelBot;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBotNet.Models;
using TelegramBotNet.States;

namespace TelegramBotNet
{
    public class BotClient
    {
        private static readonly object instanceLock = new object();

        private static volatile BotClient instance;

        public static BotClient Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new BotClient();
                    }
                }
                return instance;
            }
        }

        public Telegram.Bot.ITelegramBotClient Bot { get; }

        public PlatformClient PlatformClient { get; }
        public ProcessServiceClient ProcessServiceClient { get; }

        private readonly string _logChannelId;

        public BotClient()
        {
            var configuration = ConfigurationModel.GetConfiguration();
            //_logChannelId = configuration.TelegramConfiguration.LogChannelId;
            WebProxy proxy = new WebProxy(configuration.ProxyConfiguration.Host, configuration.ProxyConfiguration.Port);
            //var wc = new WebClient();
            //wc.Proxy = proxy;
            // var resp = wc.DownloadString("http://www.holidaywebservice.com/HolidayService_v2/HolidayService2.asmx?wsdl");
            Bot = new TelegramBotClient(configuration.TelegramConfiguration.BotToken, proxy);
            //var r = bot.TestApiAsync().GetAwaiter().GetResult();
            PlatformClient = new PlatformClient(configuration);
            ProcessServiceClient = new ProcessServiceClient(configuration);
            Bot.OnMessage += ProcessMessage;
            Bot.OnCallbackQuery += BotOnOnCallbackQuery;
            Bot.StartReceiving();
        }

        private void BotOnOnCallbackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var query = callbackQueryEventArgs.CallbackQuery;
            query.Message.Text = query.Data;
            ProcessMessage(query.Message);
        }

        private void ProcessMessage(object sender, MessageEventArgs messageEventArgs)
        {
            Message message = messageEventArgs.Message;
            try
            {
                ProcessMessage(message);
            }
            catch (Exception e)
            {
                //BotClient.SendTextMessageAsync(_logChannelId, $"Command: \"{message.Text}\".\n" +
                //                                                                            $"User: {message.Chat.FirstName} {message.Chat.LastName}.\n" +
                //                                                                            $"Message: {e.Message}\n" +
                //                                                                            $"StackTrace: {e.StackTrace}.");
            }

        }

        private void ProcessMessage(Message message)
        {
            var chatId = message.Chat.Id;
            if (message.Text.Equals(ContextCommands.Clear, StringComparison.OrdinalIgnoreCase))
            {
                ClearData(chatId);
            }

            StateContext.TrySetByMessage(chatId, message.Text);

            var stateContext = StateContext.Get(chatId);
            if (stateContext == UserContext.Undefined)
            {
                Bot.SendTextMessageAsync(chatId, Texts.Greeting, replyMarkup: Markups.StartingMarkup);
            }
            else
            {
                var contextObject = ContextStorage.GetOrAdd(stateContext, chatId);
                contextObject.Request(message);
            }
        }

        public void ClearData(long chatId)
        {
            StateContext.Clear(chatId);
            ContextStorage.Clear(chatId);
        }
    }
}