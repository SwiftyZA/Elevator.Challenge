// See https://aka.ms/new-console-template for more information

using Elevator.Challenge.Core;

Console.WriteLine("Welcome to Elevator Action!");

Console.Write("Set the number of Elevators: ");
int nrOfElevators;

while (!int.TryParse(Console.ReadLine(), out nrOfElevators))
{
    Console.WriteLine("Numeric values only please");
    Console.Write("Set the number of Elevators: ");
}

Console.Write("Set the maximum capacity of the Elevators: ");
int maxPax;

while (!int.TryParse(Console.ReadLine(), out maxPax))
{
    Console.WriteLine("Numeric values only please");
    Console.Write("Set the maximum capacity of the Elevators: ");
}

Console.Write("How many floors does the building have? ");
int topFloorNr;

while (!int.TryParse(Console.ReadLine(), out topFloorNr))
{
    Console.WriteLine("Numeric values only please");
    Console.Write("How many floors does the building have? ");
}

Console.Write("Set the number of Passengers: ");
int nrOfPassengers;

while (!int.TryParse(Console.ReadLine(), out nrOfPassengers))
{
    Console.WriteLine("Numeric values only please");
    Console.Write("Set the number of Passengers: ");
}

Console.Write("Set the tick rate (ms): ");
int tickRate;

while (!int.TryParse(Console.ReadLine(), out tickRate))
{
    Console.WriteLine("Numeric values only please");
    Console.Write("Set the tick rate (ms): ");
}

var director = // TEST CASES
               //new ElevatorDirector(5, 70, 10, 5, 500);
               //new ElevatorDirector(1, 20, 10, 5, 500);
               //new ElevatorDirector(2, 30, 10, 5, 500);
    new ElevatorDirector(nrOfElevators, nrOfPassengers, topFloorNr, maxPax, tickRate);

await director.StartAsync();


Console.ReadLine();