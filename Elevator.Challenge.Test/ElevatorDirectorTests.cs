using Elevator.Challenge.Core;
using Elevator.Challenge.Core.Services;
using Elevator.Challenge.Core.Services.Contracts;
using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;
using Elevator.Challenge.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Challenge.Test
{
    public class ElevatorDirectorTests : IClassFixture<SettingsFixture>
    {
        private readonly AppSettings _appSettings;
        private readonly List<PassengerModel> _passengers;
        public ElevatorDirectorTests(SettingsFixture settingsFixture)
        {
            _appSettings = settingsFixture.AppSettings;

            _passengers = new List<PassengerModel>
            { 
                new PassengerModel
                {
                    Id = 1,
                    Destination = 10,
                    Origin = 0,
                    TravelDirection = Direction.Up,
                },
                new PassengerModel
                {
                    Id = 2,
                    Destination = 11,
                    Origin = 5,
                    TravelDirection = Direction.Up,
                },
                new PassengerModel
                {
                    Id = 3,
                    Destination = 0,
                    Origin = 20,
                    TravelDirection = Direction.Down,
                },
                new PassengerModel
                {
                    Id = 4,
                    Destination = 0,
                    Origin = 17,
                    TravelDirection = Direction.Down
                }
            };
        }

        [Fact]
        public void Get_Directive_Test()
        {
            var director = new ElevatorDirectorService(_appSettings, new StateService());

            director.AddPassengers(_passengers);

            var floor_0 = director.GetDirective(2);
            Assert.True(floor_0 == 0);

            var floor_5 = director.GetDirective(10);
            Assert.True(floor_5 == 5);

            var floor_17 = director.GetDirective(12);
            Assert.True(floor_17 == 17);

            var floor_20 = director.GetDirective(19);
            Assert.True(floor_20 == 20);
        }
    }
}
