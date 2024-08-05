namespace Elevator.Challenge.Models
{
    internal class StateModel
    {
        public IEnumerable<FloorStateModel> FloorStates { get; set; }
        public IEnumerable<ElevatorStateModel> ElevatorStates { get; set; }
    }
}
