using System;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using Microsoft.Extensions.Configuration;

namespace FileGrowthService.Csv
{
    public class FileGrowthCsvWriterProvider : IFileGrowthWriterProvider
    {
        public FileGrowthCsvWriterProvider(IConfiguration configuration, IFileStreamProvider fileStreamProvider)
        {
            Configuration = configuration;
            FileStreamProvider = fileStreamProvider;
        }

        IConfiguration Configuration { get; }

        IFileStreamProvider FileStreamProvider { get; }

        public void WriteDenormalisedFileGrowthStats(FileMetaData metaData, FileSizeStats fileSizeStats, FileGrowthStats fileGrowthStats)
        {
            var utf8 =  new UTF8Encoding(false);
            var filePath = Path.Combine(Configuration["WorkingDirectory"], $"{metaData.FileID}.csv");

            using (var fileStream = FileStreamProvider.OpenWrite(filePath))
            using (var textWriter = new StreamWriter(fileStream, utf8, 1024 * 10, leaveOpen: true))
            using (var csvWriter = new CsvWriter(textWriter, leaveOpen: true))
            {
                // write headers as fields as we need to use custom quoting and mixed types
                csvWriter.WriteField(nameof(FileMetaData.FileID), shouldQuote: true);
                csvWriter.WriteField(nameof(FileMetaData.Name), shouldQuote: true);
                csvWriter.WriteField(nameof(FileSizeStatsDto.Timestamp), shouldQuote: true);
                csvWriter.WriteField(nameof(FileSizeStatsDto.SizeInBytes), shouldQuote: true);
                csvWriter.WriteField("GrowthRatesInBytesPerHour", shouldQuote: true);
                csvWriter.NextRecord();

                foreach (var dto in fileGrowthStats)
                {
                    csvWriter.WriteField(metaData.FileID);
                    csvWriter.WriteField(metaData.Name, shouldQuote: true);
                    csvWriter.WriteField(FormatTime(dto.Key), shouldQuote: true);
                    // obtain file size from original data set
                    csvWriter.WriteField(fileSizeStats[dto.Key]);
                    csvWriter.WriteField(FormatHourlyGrowthRate(dto.Value));
                    csvWriter.NextRecord();
                }
            }
        }

        /// <summary>
        /// Use bankers rounding to round the number to one decimal digit
        /// </summary>
        public static double RoundHourlyGrowthRate(double rate)
            => Math.Round(rate, 1, MidpointRounding.ToEven);

        /// <summary>
        /// Round the given time in a culture invariant way including date, time and
        /// milliseconds.
        /// </summary>
        public static string FormatTime(DateTime time)
            => time.ToString(@"yyyy-MM-dd HH\:mm\:ss\.fff", CultureInfo.InvariantCulture);

        /// <summary>
        /// Round and format hourly growth rate using digit precision.
        /// </summary>
        public static string FormatHourlyGrowthRate(double rate)
            => RoundHourlyGrowthRate(rate).ToString("0.0", CultureInfo.InvariantCulture);
    }
}