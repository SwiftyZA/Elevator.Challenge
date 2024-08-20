using Elevator.Challenge.Domain.Contracts;

namespace Elevator.Challenge.Domain.Models
{
    public class AppSettings : IAppSettings
    {
        public int ElevatorCount { get; set; }

        public int PassengerCount { get; set; }

        public int TopFloorNr { get; set; }

        public int MaxPax { get; set; }

        public int TickRate { get; set; }
    }
}
