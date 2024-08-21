using Elevator.Challenge.Core.Factories;
using Elevator.Challenge.Core.Managers;
using Elevator.Challenge.Core.Services.Contracts;
using Elevator.Challenge.Domain.Contracts;
using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core
{
    internal class ElevatorDirectorService : IElevatorDirectorService
    {
        private bool disposedValue;
        private List<ElevatorManager> _elevators;
        private List<FloorManager> _floorManagers;
        private readonly IAppSettings _appSettings;
        private IStateService _stateService;
        private Timer _timer;

        public ElevatorDirectorService(IAppSettings appSettings, IStateService stateService)
        {
            _appSettings = appSettings;
            _stateService = stateService;

            _elevators = ElevatorFactory.GenerateElevators(appSettings, PickUpPassengers, GetDirective).ToList();
            var _passengers = PassengerFactory.GeneratePassengers(appSettings).ToList();
            _floorManagers = FloorManagerFactory.GenerateFloorManagers(_passengers, appSettings.TopFloorNr).ToList();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var randomness = new Random();
            _timer = new Timer(ReportState, null, 0, 500);
            // Initial delay so we can see the lay of the land before the pieces start moving
            await Task.Delay(1000);
            _elevators.ForEach(async x =>
            {
                x.Start();
                // Stagger the start
                await Task.Delay(randomness.Next(100, 1000));
            });
        }

        private void ReportState(object? state)
        {
            var _state = new StateModel
            {
                ElevatorStates = _elevators.Select(x => new ElevatorStateModel { Floor = x.CurrentFloor, Passengers = x.PassengerCount }),
                FloorStates = _floorManagers.Select(x => new FloorStateModel { FloorNr = x.FloorNumber, Passengers = x.PassengerCount })
            };

            _stateService.ReportState(_state);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _elevators.ForEach(_x => _x.GracefullStop());
        }

        private IEnumerable<PassengerModel> PickUpPassengers(Direction direction, int elevatorFloor, int availableSpots)
        {
            var floor = _floorManagers.FirstOrDefault(x => x.FloorNumber == elevatorFloor);

            if (floor != null)
            {
                var we_happy_few = floor.GetPassengers(availableSpots, direction);
                return we_happy_few;
            }

            return Enumerable.Empty<PassengerModel>();
        }

        public void AddPassengers(IEnumerable<PassengerModel> incomingLemmings)
        {
            var floors = incomingLemmings.Select(x => x.Origin).Distinct().ToList();
            floors.ForEach(floor =>
            {
                _floorManagers.FirstOrDefault(x => x.FloorNumber == floor)?
                    .AddPassengers(incomingLemmings.Where(x => x.Origin == floor).ToList());
            });
        }

        // This could result in multiple empty elevalors heading to the same floor, but is in line with real world behavior.
        // There is some room for improvement here by keeping track of floors with an elevator en route.
        private int GetDirective(int elevatorLocation)
        {
            var floors = _floorManagers.Where(x => x.PassengerCount > 0)
                .Select(x =>
                {
                    var diff = Math.Abs(elevatorLocation - x.FloorNumber);
                    return new { x.FloorNumber, diff };
                }).OrderBy(x => x.diff).ToList();

            return floors.FirstOrDefault()?.FloorNumber ?? -1;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                    _elevators.ForEach(x => x.Dispose());
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        
    }
}
