using Elevator.Challenge.Core.Managers;
using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;
using Elevator.Challenge.Test.Fixtures;

namespace Elevator.Challenge.Test
{
    public class ElevatorManagerTests : IClassFixture<SettingsFixture>
    {
        private readonly AppSettings _appSettings;
        public ElevatorManagerTests(SettingsFixture settingsFixture)
        {
            _appSettings = settingsFixture.AppSettings;
        }

        [Fact]
        public async Task Elevator_Up_Direction_Test()
        {
            var passenger = new PassengerModel
            {
                Id = 1,
                Destination = 10,
                Origin = 0,
                TravelDirection = Direction.Up
            };

            var elevator = new ElevatorModel
            {
                Id = 1,
                MaxPax = _appSettings.MaxPax,
                CurrentFloor = 0,
                Passengers = new List<PassengerModel> { passenger },
            };

            var manager = new ElevatorManager(elevator, Mock_PickUpPassengers_Up, Mock_GetDirective, _appSettings.TickRate);
            
            manager.Start();

            do
            {
                await Task.Delay(100);
            } while (manager.Direction != Direction.Idle);

            manager.GracefullStop();
            manager.Dispose();
        }

        [Fact]
        public async Task Elevator_Down_Direction_Test()
        {
            var passenger = new PassengerModel
            {
                Id = 2,
                Destination = 0,
                Origin = 10,
                TravelDirection = Direction.Down
            };

            var elevator = new ElevatorModel
            {
                Id = 1,
                MaxPax = _appSettings.MaxPax,
                CurrentFloor = 10,
                Passengers = new List<PassengerModel> { passenger },
            };

            var manager = new ElevatorManager(elevator, Mock_PickUpPassengers_Down, Mock_GetDirective, _appSettings.TickRate);

            manager.Start();

            do
            {
                await Task.Delay(100);
            } while (manager.Direction != Direction.Idle);

            manager.GracefullStop();
            manager.Dispose();
        }

        private IEnumerable<PassengerModel> Mock_PickUpPassengers_Up(Direction direction, int elevatorFloor, int availableSpots)
        {
            Assert.True(direction == Direction.Up || availableSpots == _appSettings.MaxPax);
            return Enumerable.Empty<PassengerModel>();
        }

        private IEnumerable<PassengerModel> Mock_PickUpPassengers_Down(Direction direction, int elevatorFloor, int availableSpots)
        {
            Assert.True(direction == Direction.Down || availableSpots == _appSettings.MaxPax);
            return Enumerable.Empty<PassengerModel>();
        }

        private int Mock_GetDirective(int elevatorLocation)
        {
            return 0;
        }
    }
}
