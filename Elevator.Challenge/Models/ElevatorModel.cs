using Elevator.Challenge.Enums;

namespace Elevator.Challenge.Models
{
    internal class ElevatorModel
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public int MaxPax { get; set; }
        public List<PassengerModel> Passengers { get; set; }
    }
}
