using System.Text;

namespace Elevator.Challenge.Helpers
{
    internal static class Picaso
    {
        internal static void DrawBuilding(int floors, int elevators)
        {
            Console.Clear();
            // Headers
            Console.WriteLine($"_______________________{BuildElevatorSection(elevators, "______")}");
            Console.WriteLine($"| Floors | Passengers |{AddElevatorHeaders(elevators)}");
            Console.WriteLine($"|________|____________|{BuildElevatorSection(elevators, "_____|")}");

            for (int i = floors - 1; i >= 0; i--) 
            {
                Console.WriteLine($"|  {GetFloorText(i)}   |            |{BuildElevatorSection(elevators, "     |")}");
                Console.WriteLine($"|________|____________|{BuildElevatorSection(elevators, "_____|")}");
            }
        }

        private static string GetFloorText(int i)
        {
            return i.ToString().PadRight(3, ' ');
        }

        private static string BuildElevatorSection(int elevatorCount, string section)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < elevatorCount; i++) 
                sb.Append(section);
            return sb.ToString();
        }

        private static string AddElevatorHeaders(int elevatorCount)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < elevatorCount; i++)
                sb.Append($" E.{i} |");
            return sb.ToString();
        }
    }
}
