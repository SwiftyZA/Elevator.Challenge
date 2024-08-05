﻿using Elevator.Challenge.Core;
using Elevator.Challenge.Models;

namespace Elevator.Challenge.Factories
{
    internal  static class FloorManagerFactory
    {
        internal static IEnumerable<FloorManager> GenerateFloorManagers(IEnumerable<PassengerModel> passengers, int nrOfFloors)
        {
            return Enumerable.Range(0, nrOfFloors).Select(floor_nr => {
                var lemmings = passengers.Where(x => x.Origin == floor_nr).ToList();
                return new FloorManager(floor_nr, lemmings);
            });
        }
    }
}
