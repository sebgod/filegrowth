File Growth Service
===================

This project demonstrates how a readable, composable, extensible and maintainable service can be created using the ]latest .NET Core framework](https://www.microsoft.com/net/download/dotnet-core/2.1).
It is essential to update to at least version 2.1 otherwise unit testing might not work.
The project relies on .NET Core integrated DI, which is heavily used by the ASP.NET Core framework and thus right-sized for this project.
As this is a PoC project, emphasis is on correctness, readability and code coverage. Given the time constrains not every member has a proper code documation annotation.

## Code layout

The code is composed of several sub projects:

  * [FileGrowthConsoleApp](FileGrowthConsoleApp)
    A console based host that initialises the [Application](FileGrowthService.App/Application.cs)
  * [FileGrowthService](FileGrowthService)
    Contains implemenations of [IFileGrowthMeasureService](FileGrowthService.Abstractions/FileGrowthService.cs)
    and [IFileGrowthMeasureProcessor](FileGrowthService.Abstractions/FileGrowthMeasureProcessor.cs)
  * [FileGrowthService.Abstractions](FileGrowthService.Abstractions)
    This assembly contains all basic immutable types and all DI interfaces. Referenced by all other projects.
    - [IFileGrowthMeasureService](FileGrowthService.Abstractions/FileGrowthService.cs)
      Reads, processes and writes denomalised file growth data
    - [IFileGrowthMeasureProcessor](FileGrowthService.Abstractions/FileGrowthProcessor.cs)
      Responsible for measuring file growth per hour given input from the reader
    - [IFileGrowthReaderProvider](FileGrowthService.Abstractions/FileGrowthReaderProvider.cs)
      Provides a facility for reading file growth facts
    - [IFileGrowthWriterProvider](FileGrowthService.Abstractions/FileGrowthWriterProvider.cs)
      Provides a facility for writing denomalised file growth facts with processed data (i.e. growth per hour) 
  * [FileGrowthService.App](FileGrowthService.App)
    The sole class [Application](FileGrowthService.App/Application.cs) is the hostable entry point that initialises
    [IFileGrowthMeasureService](FileGrowthService.Abstractions/FileGrowthService.cs) and its dependencies.
  * [FileGrowthService.Csv](FileGrowthService.Csv)
    Implementation for both [IFileGrowthReaderProvider](FileGrowthService.Abstractions/FileGrowthReaderProvider.cs)
    and [IFileGrowthWriterProvider](FileGrowthService.Abstractions/FileGrowthWriterProvider.cs) based
    on [CsvHelper](https://joshclose.github.io/CsvHelper/).
  * [FileGrowthService.File](FileGrowthService.File)
    Is used by the CSV read/write implementation to open and save files.
  * [FileGrowthService.Tests](FileGrowthService.Tests)
    Unit testing based on [NUnit](https://github.com/nunit/docs/wiki/NUnit-Documentation) and [Moq](https://github.com/moq/moq4/wiki/Quickstart) with :100:% coverage of all
    DI interfaces in [FileGrowthService.Abstractions](FileGrowthService.Abstractions) and the corresponding implementations.
    
## Build instructions

Either use [Visual Studio Code](https://code.visualstudio.com/) and open the project at the project root,
or [Visual Studio 2017 15.8](https://visualstudio.microsoft.com/vs/) to open the [solution file](FileGrowth.sln).
Command line building is also supported by running [build.cmd](build.cmd).
Additional commands supplied are [run.cmd](run.cmd), which runs the [FileGrowthConsoleApp](FileGrowthConsoleApp),
[clean.cmd](clean.cmd), which deletes all generated CSV files and build artifacts, and [test.cmd](test.cmd) which runs the NUnit test suite.
