using Elevator.Challenge.Core.Factories;
using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;
using Elevator.Challenge.Test.Fixtures;

namespace Elevator.Challenge.Test
{
    public class FactoryTests : IClassFixture<SettingsFixture>
    {
        const int count_5 = 5;
        const int count_15 = 15;
        const int count_100 = 100;

        private readonly AppSettings _appSettings;

        public FactoryTests(SettingsFixture settingsFixture)
        {
            _appSettings = settingsFixture.AppSettings;
        }

        [Fact]
        public void Elevator_Factory_Output_Count_Test()
        {
            _appSettings.ElevatorCount = count_5;
            var five = ElevatorFactory.GenerateElevators(_appSettings, Mock_PickUpPassengers, Mock_GetDirective);
            Assert.True(five.Count() == count_5, $"Failed at count {count_5}");

            _appSettings.ElevatorCount = count_15;
            var fifteen = ElevatorFactory.GenerateElevators(_appSettings, Mock_PickUpPassengers, Mock_GetDirective);
            Assert.True(fifteen.Count() == count_15, $"Failed at count {count_15}");

            _appSettings.ElevatorCount = count_100;
            var hundred = ElevatorFactory.GenerateElevators(_appSettings, Mock_PickUpPassengers, Mock_GetDirective);
            Assert.True(hundred.Count() == count_100, $"Failed at count {count_100}");
        }

        [Fact]
        public void Passenger_Factory_Output_Count_Test()
        {
            _appSettings.PassengerCount = count_5;
            var five = PassengerFactory.GeneratePassengers(_appSettings);
            Assert.True(five.Count() == count_5, $"Failed at count {count_5}");

            _appSettings.PassengerCount = count_15;
            var fifteen = PassengerFactory.GeneratePassengers(_appSettings);
            Assert.True(fifteen.Count() == count_15, $"Failed at count {count_15}");

            _appSettings.PassengerCount = count_100;
            var hundred = PassengerFactory.GeneratePassengers(_appSettings);
            Assert.True(hundred.Count() == count_100, $"Failed at count {count_100}");
        }

        [Fact]
        public void FloorManager_Factory_Output_Count_Test()
        {
            var five = FloorManagerFactory.GenerateFloorManagers(new List<PassengerModel>(), count_5);
            Assert.True(five.Count() == count_5, $"Failed at count {count_5}");

            var fifteen = FloorManagerFactory.GenerateFloorManagers(new List<PassengerModel>(),count_15);
            Assert.True(fifteen.Count() == count_15, $"Failed at count {count_15}");

            var hundred = FloorManagerFactory.GenerateFloorManagers(new List<PassengerModel>(), count_100);
            Assert.True(hundred.Count() == count_100, $"Failed at count {count_100}");
        }

        private IEnumerable<PassengerModel> Mock_PickUpPassengers(Direction direction, int elevatorFloor, int availableSpots)
        {
            return Enumerable.Empty<PassengerModel>();
        }

        private int Mock_GetDirective(int elevatorLocation)
        {
            return 0;
        }
    }
}
