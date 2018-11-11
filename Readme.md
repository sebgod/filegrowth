File Growth Service
===================

This project demonstrates how a readable, composable, extensible and maintainable service can be created using the latest .NET Core framework.
The project relies on .NET Core integrated DI, which is heavily used by the ASP.NET Core framework

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
    [NUnit](https://github.com/nunit/docs/wiki/NUnit-Documentation) based unit testing with 100% coverage of all
    DI interfaces in [FileGrowthService.Abstractions](FileGrowthService.Abstractions) and the corresponding implementations.
