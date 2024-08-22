using Elevator.Challenge.Core.Factories;
using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;
using Elevator.Challenge.Test.Fixtures;

namespace Elevator.Challenge.Test
{
    public class PassengerTests : IClassFixture<SettingsFixture>
    {
        const int count_100 = 100;

        private readonly AppSettings _appSettings;
        public PassengerTests(SettingsFixture settingsFixture)
        {
            _appSettings = settingsFixture.AppSettings;
        }

        [Fact]
        public void Passenger_Direction_Test()
        {
            _appSettings.PassengerCount = count_100;
            var hundred = PassengerFactory.GeneratePassengers(_appSettings);

            foreach (var passenger in hundred) 
            {
                if (passenger.TravelDirection == Direction.Up)
                    Assert.True(passenger.Origin < passenger.Destination, $"Incorrection direction: {passenger.TravelDirection.ToString()} | {passenger.Origin} to {passenger.Destination} ");
                else if (passenger.TravelDirection == Direction.Down)
                    Assert.True(passenger.Origin > passenger.Destination, $"Incorrection direction: {passenger.TravelDirection.ToString()} | {passenger.Origin} to {passenger.Destination} ");
            }
        }
    }
}
