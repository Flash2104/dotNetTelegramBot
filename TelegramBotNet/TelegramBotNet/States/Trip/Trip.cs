using Telegram.Bot.Types;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class Trip : IContextObject
    {
        public TripModel TripModel { get; }

        public TripState State { get; }

        public Trip()
        {
            TripModel = new TripModel();
            State = new CreateNewTripState(TripModel);
        }

        public void Request(Message message)
        {
            if (State.HandleMessage(State, message))
            {
                State.SendInvitation(message);
            }
        }
    }
}