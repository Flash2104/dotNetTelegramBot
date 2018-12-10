using Telegram.Bot.Types;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class WaitForCommandState: TripState
    {
        public WaitForCommandState(TripModel model) 
            : base(model)
        {
        }

        public override bool HandleMessage(IState state, Message message)
        {
            throw new System.NotImplementedException();
        }

        public override void SendInvitation(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}