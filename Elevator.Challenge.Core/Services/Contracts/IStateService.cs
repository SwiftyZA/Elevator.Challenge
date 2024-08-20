using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core.Services.Contracts
{
    public interface IStateService
    {
        void ReportState(StateModel state);
    }
}
