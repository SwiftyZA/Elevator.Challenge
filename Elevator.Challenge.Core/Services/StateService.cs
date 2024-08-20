//using Elevator.Challenge.Helpers;
using Elevator.Challenge.Domain.Models;

namespace Elevator.Challenge.Core.Services
{
    internal class StateService
    {
        private bool _initialDrawDone = false;
        private bool _lastDrawDone = true;
        internal void ReportState(StateModel state)
        {
            Console.CursorVisible = false;
            if (!_initialDrawDone)
            {
                //Picaso.DrawBuilding(state.FloorStates.Count(), state.ElevatorStates.Count());
                _initialDrawDone = true;
            }

            UpdateFloors(state);
            ClearShafts(state);
            UpdateElevators(state);
        }

        private void UpdateElevators(StateModel state)
        {
            for (int i = 0; i < state.ElevatorStates.Count(); i++)
            {
                var y_pos = 1 + ((state.FloorStates.Count() - state.ElevatorStates.ToArray()[i].Floor) * 2);
                var x_pos = 24 + (i * 6);
                Console.SetCursorPosition(x_pos, y_pos);
                Console.Write($"P.{state.ElevatorStates.ToArray()[i].Passengers}");
            }
        }

        private void ClearShafts(StateModel state)
        {
            for (int y = 0; y < state.FloorStates.Count(); y++)
            {
                var y_pos = 3 + (y * 2);

                for (int x = 0; x < state.ElevatorStates.Count(); x++)
                {
                    var x_pos = 24 + (x * 6);

                    Console.SetCursorPosition(x_pos, y_pos);
                    Console.Write("   ");
                }
            }
        }

        // This method throws an error on my main machine when more than 16 floors are entered.
        // The y axis is incorrect on the Console, line one has a value of -2, line 2 is -1 and line 3 is 0.
        // I tested this on other machines and did not encounter the issue. I was able to push the numbers much higher.
        private void UpdateFloors(StateModel state)
        {
            for (int i = state.FloorStates.Count() - 1; i >= 0; i--)
            {
                var y_pos = 1 + ((state.FloorStates.Count() - i) * 2);
                Console.SetCursorPosition(14, y_pos);
                Console.WriteLine($"{state.FloorStates.ToArray()[i].Passengers}  ");
            }
        }
    }
}
