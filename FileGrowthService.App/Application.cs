using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileGrowthService;
using FileGrowthService.Csv;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileGrowthService.App
{
    public class Application
    {
        public IServiceProvider Services { get; set; }

        public Application(IServiceCollection serviceCollection)
        {
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IConfiguration>(BuildConfiguration())
                .AddSingleton<IFileGrowthReaderProvider, FileGrowthCsvReaderProvider>()
                .AddSingleton<IFileGrowthWriterProvider, FileGrowthCsvWriterProvider>()
                .AddSingleton<IFileGrowthMeasureService, FileGrowthMeasureService>();
        }

        public void ProcessFiles()
        {
            var measureService = Services.GetRequiredService<IFileGrowthMeasureService>();
            measureService.ProcessFiles();
        }

        static IReadOnlyDictionary<string, string> DefaultConfigurationStrings { get; } =
            new Dictionary<string, string>()
            {
                ["WorkingDirectory"] = Directory.GetCurrentDirectory(),
                ["FileIDName"] = "Files.csv",
                ["FileStatsName"] = "FileStats.csv"
            };
        /// <summary>
        /// Build the default configuration, based on predefined values and an optional
        /// config.json file.
        /// </summary>
        private IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .AddInMemoryCollection(DefaultConfigurationStrings)
                .Build();
    }
}
