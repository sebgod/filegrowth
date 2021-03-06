﻿using System;
using System.Collections.Generic;
using System.IO;
using FileGrowthService.Csv;
using FileGrowthService.File;
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
                .AddSingleton(BuildConfiguration())
                .AddTransient<IFileStreamProvider, FileStreamProvider>()
                .AddTransient<IFileGrowthReaderProvider, FileGrowthCsvReaderProvider>()
                .AddTransient<IFileGrowthWriterProvider, FileGrowthCsvWriterProvider>()
                .AddTransient<IFileGrowthMeasureProcessor, FileGrowthMeasureProcessor>()
                .AddTransient<IFileGrowthMeasureService, FileGrowthMeasureService>();
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
        /// Build the default configuration, based on predefined values and from the environment,
        /// using the prefix FGS_
        /// </summary>
        private IConfiguration BuildConfiguration() =>
            new ConfigurationBuilder()
                .AddInMemoryCollection(DefaultConfigurationStrings)
                .AddEnvironmentVariables(prefix: "FGS_")
                .Build();
    }
}
