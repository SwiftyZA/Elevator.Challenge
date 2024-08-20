using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core.Managers
{
    internal class FloorManager
    {
        public int FloorNumber { get; }
        public int PassengerCount => _passengers.Count;

        // Using a regular list with a lock here instead of a ConcurrentDictionary since a
        // ConcurrentDictionary does not allow if popping multiples
        private List<PassengerModel> _passengers;
        private object _lock = new object();

        public FloorManager(int floorNumber, IEnumerable<PassengerModel> passengers)
        {
            FloorNumber = floorNumber;
            _passengers = passengers.ToList();
        }

        public IEnumerable<PassengerModel> GetPassengers(int availableSpots, Direction direction)
        {
            lock (_lock)
            {
                if (!_passengers.Any())
                    return new List<PassengerModel>();

                // The first one in always gets to choose the direction
                if (direction == Direction.Idle)
                    direction = _passengers.FirstOrDefault().TravelDirection;

                var we_happy_few = _passengers.Where(x => x.TravelDirection == direction).Take(availableSpots).ToList();
                we_happy_few.ForEach(x => _passengers.Remove(x));

                return we_happy_few;
            }
        }

        public void AddPassengers(IEnumerable<PassengerModel> new_Lemmings)
        {
            lock (_lock) 
            {
                _passengers?.AddRange(new_Lemmings);
            }
        }
    }
}
