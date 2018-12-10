using Telegram.Bot.Types;
using TelegramBotNet.Models;

namespace TelegramBotNet.States
{
    public class CreateNewTripState : TripState
    {
        public CreateNewTripState(TripModel model)
            : base(model)
        {
            StateKind = TripStateKind.CreateNewTrip;
        }

        public override bool HandleMessage(IState state, Message message)
        {
            bool result = false;
            if (_innerState == null)
            {
                _innerState = new ChangingDestinationState(_model);
                return true;
            }
            if (_innerState.HandleMessage(_innerState, message))
            {
                GoNextStep();
                result = true;
            }
            return result;
        }

        public override void SendInvitation(Message message)
        {
            _innerState.SendInvitation(message);
        }

        private void GoNextStep()
        {
            switch (_innerState.StateKind)
            {
                case TripStateKind.ChangingDestination:
                {
                    _innerState = new ChangingOrganizationState(_model);
                    break;
                }
                case TripStateKind.ChangingOrganization:
                {
                    _innerState = new ChangingStartDate(_model);
                    break;
                }
                case TripStateKind.ChangingStartDate:
                {
                    _innerState = new ChangingEndDate(_model);
                    break;
                }
                case TripStateKind.ChangingEndDate:
                {
                    _innerState = new FinishCreationState(_model);
                    break;
                }
                default:
                {
                    _innerState = new FinishCreationState(_model);
                    break;
                }
            }
        }
    }
}