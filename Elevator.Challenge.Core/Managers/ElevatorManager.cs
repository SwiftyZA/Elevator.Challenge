using Elevator.Challenge.Domain.Enums;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core.Managers
{
    public class ElevatorManager : IDisposable
    {
        private int _tickRate;
        private CancellationTokenSource _tokenSource;
        private Task _engine;
        private bool disposedValue;
        private bool _stop = false;
        private int _directiveFloor = -1;
        private ElevatorModel _elevator;

        private Func<Direction, int, int, IEnumerable<PassengerModel>> _pickup;
        private Func<int, int> _getDirective;

        public int CurrentFloor => _elevator.CurrentFloor;
        public int PassengerCount => _elevator.Passengers.Count;

        public Direction Direction
        {
            get
            {
                // All passengers will have the same Direction, so use the first passenger's direction
                if (_elevator.Passengers.Any())
                    return _elevator.Passengers.First().TravelDirection;

                // If the elevator has been sent somewhere (and not there yet), determine the direction by comparing the origin and destination
                if (_directiveFloor > -1 && _directiveFloor != _elevator.CurrentFloor)
                    return _elevator.CurrentFloor < _directiveFloor ? Direction.Up : Direction.Down;

                // Else idle
                return Direction.Idle;
            }
        }

        public ElevatorManager(ElevatorModel elevator, Func<Direction, int, int, IEnumerable<PassengerModel>> pickup, Func<int, int> getDirective, int tickRate)
        {
            _elevator = elevator;
            _tickRate = tickRate;
            _pickup = pickup;
            _getDirective = getDirective;
            _tokenSource = new CancellationTokenSource();
        }

        private async void DoElevatorStuff()
        {
            try
            {
                do
                {
                    // Let people get off
                    var offload = _elevator.Passengers.Where(x => x.Destination == _elevator.CurrentFloor).ToList();
                    if (offload.Any())
                        offload.ForEach(x => _elevator.Passengers.Remove(x));

                    // Let people get on
                    // People can get on if the elevator has not been directed somewhere and there is space available
                    if ((_elevator.Passengers.Count() < _elevator.MaxPax && _directiveFloor == -1)
                        || _directiveFloor == _elevator.CurrentFloor) // OR the elevator has arrived at the directed location
                    {
                        var spots = _elevator.MaxPax - _elevator.Passengers.Count();
                        _elevator.Passengers.AddRange(_pickup(Direction, _elevator.CurrentFloor, spots));

                        // If active reset directive floor
                        if (_directiveFloor == _elevator.CurrentFloor)
                            _directiveFloor = -1;  
                    }

                    // If no passengers are onboard and no directive is active, get a new directive
                    if (!_elevator.Passengers.Any() && _directiveFloor == -1)
                        _directiveFloor = _getDirective(_elevator.CurrentFloor);

                    // Get a move on
                    if (Direction == Direction.Up)
                        _elevator.CurrentFloor++;
                    else if (Direction == Direction.Down)
                        _elevator.CurrentFloor--;
                    // else stay idle and wait for new passengers

                    // Wait for next tick
                    await Task.Delay(_tickRate);
                } while (!_stop);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Start()
        {
            _engine = Task.Run(DoElevatorStuff, _tokenSource.Token);
        }

        public void GracefullStop()
        {
            _stop = true;
        }
        public void Stop()
        {
            _tokenSource?.Cancel();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Stop();
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
