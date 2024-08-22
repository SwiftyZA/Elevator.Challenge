using Elevator.Challenge.Domain.Contracts;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Test.Fixtures
{
    public class SettingsFixture
    {
        public AppSettings AppSettings { get; }
        public SettingsFixture()
        {
            AppSettings = new AppSettings
            {
                ElevatorCount = 0,
                MaxPax = 5,
                PassengerCount = 0,
                TickRate = 10,
                TopFloorNr = 21
            };
        }
    }
}
