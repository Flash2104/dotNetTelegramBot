using Telegram.Bot.Types;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public abstract class TripState: IState
    {
        protected readonly TripModel _model;

        protected TripState _innerState;

        public TripStateKind StateKind { get; protected set; }

        protected TripState(TripModel model)
        {
            _model = model;
        }

        public abstract bool HandleMessage(IState state, Message message);
        public abstract void SendInvitation(Message message);
    }

    public enum TripStateKind
    {
        Undefined,
        ChangingDestination,
        ChangingOrganization,
        ChangingStartDate,
        ChangingEndDate,
        EditingTrip, 
        WaitForCommand,
        CreateNewTrip,
        FinishCreation
    }
}