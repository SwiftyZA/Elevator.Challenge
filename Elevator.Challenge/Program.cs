// See https://aka.ms/new-console-template for more information
using Elevator.Challenge.Core.IoC;
using Elevator.Challenge.Domain.Contracts;
using Elevator.Challenge.Domain.Models;
using Elevator.Challenge.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

Console.Clear();


var settings = new AppSettings
{
    TickRate = tickRate,
    ElevatorCount = nrOfElevators,
    MaxPax = maxPax,
    TopFloorNr = topFloorNr,
    PassengerCount = nrOfPassengers
};

Picaso.DrawBuilding(settings.TopFloorNr, settings.ElevatorCount);

var _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IAppSettings>(settings);
                    services.AddCoreElevatorServices();
                })
                .ConfigureLogging(logging => {
                    logging.SetMinimumLevel(LogLevel.Warning);
                }).Build();

_host.Run();
