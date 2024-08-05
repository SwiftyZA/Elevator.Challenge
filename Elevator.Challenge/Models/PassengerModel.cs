using Elevator.Challenge.Enums;

namespace Elevator.Challenge.Models
{
    internal class PassengerModel
    {
        public int Id { get; set; }
        public int Origin { get; set; }
        public int Destination { get; set; }
        public Direction TravelDirection { get; set; }
    }
}
