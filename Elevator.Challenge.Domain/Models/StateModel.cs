namespace Elevator.Challenge.Domain.Models
{
    public class StateModel
    {
        public IEnumerable<FloorStateModel> FloorStates { get; set; }
        public IEnumerable<ElevatorStateModel> ElevatorStates { get; set; }
    }
}
