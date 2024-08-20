using Elevator.Challenge.Domain.Enums;

namespace Elevator.Challenge.Domain.Models
{
    public class PassengerModel
    {
        public int Id { get; set; }
        public int Origin { get; set; }
        public int Destination { get; set; }
        public Direction TravelDirection { get; set; }
    }
}
