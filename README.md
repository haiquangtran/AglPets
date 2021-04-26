# AGL Pets Solution

A simple console app that prints cats from a third party API providing the data.

The cats are under a heading of the gender of their owner and are in alphabetical order.

## Developer Requirements
- .NET 5
- Visual Studio or dotnet CLI

## Technology Stack
- .NET 5

## Design patterns used
- Uses [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- Dependency Injection
- SOLID principles

## Set up with Visual Studio
### Run the project with Visual Studio
- Clone the repository
- In Visual Studio, locate the AglPets/ folder and open the Agl.Pets.sln file in Visual Studio
- Ensure that the solution builds successfully by rebuilding the solution. selecting  Build > Rebuild.
- Set the startup project to be Agl.Pets.ConsoleApp
- Run the project (either in debug mode or without). To run in debug mode press F5, otherwise, press Ctrl + F5 to run without debug mode.
- All done!

## Set up with dotnet CLI
### Run the project with dotnet command
- open command line, navigate to solution in AglPets/ folder
- build the solution by typing in ``dotnet build``, ensure the build is successful
- Run the Web project by typing in ``dotnet run --project ./Agl.Pets.ConsoleApp``
- You should now see Cats printed in your console!
- All done!
- For more informaton see the following url: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run?tabs=netcore22

## Running tests with dotnet CLI
- open command line, navigate to solution in AglPets/ folder
- Run the unit tests typing in ``dotnet test`` in command line
- For more informaton see the following url: https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test

## Extensions
- Logging needs to be added to the solution, this can be added by using a Global exception handler.
