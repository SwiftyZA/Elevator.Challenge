namespace Elevator.Challenge.Domain.Contracts
{
    public interface IAppSettings
    {
        public int ElevatorCount { get; }
        public int PassengerCount { get; }
        public int TopFloorNr { get;  }
        public int MaxPax { get; }
        public int TickRate { get; }
    }
}
