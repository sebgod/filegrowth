using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using FileGrowthService;
using Microsoft.Extensions.Configuration;

namespace FileGrowthService.Csv
{
    public class FileGrowthCsvWriterProvider : IFileGrowthWriterProvider
    {
        public FileGrowthCsvWriterProvider(IConfiguration configuration)
            => Configuration = configuration;

        IConfiguration Configuration { get; }

        public void WriteDenormalisedFileGrowthStats(FileMetaData metaData, FileSizeStats fileSizeStats, FileGrowthStats FileGrowthStats)
        {
            var utf8 =  new UTF8Encoding(false);
            var filePath = Path.Combine(Configuration["WorkingDirectory"], $"{metaData.FileID}.csv");

            using (var textWriter = new StreamWriter(File.OpenWrite(filePath), utf8))
            using (var csvWriter = new CsvWriter(textWriter, leaveOpen: true))
            {
                // write headers as fields as we need to use custom quoting and mixed types
                csvWriter.WriteField(nameof(FileMetaData.FileID), shouldQuote: true);
                csvWriter.WriteField(nameof(FileMetaData.Name), shouldQuote: true);
                csvWriter.WriteField(nameof(FileSizeStatsDto.Timestamp), shouldQuote: true);
                csvWriter.WriteField(nameof(FileSizeStatsDto.SizeInBytes), shouldQuote: true);
                csvWriter.NextRecord();

                foreach (var dto in fileSizeStats)
                {
                    csvWriter.WriteField(metaData.FileID);
                    csvWriter.WriteField(metaData.Name, shouldQuote: true);
                    csvWriter.WriteField(dto.Key.ToString(@"yyyy-MM-dd HH\:mm\:ss\.fff", CultureInfo.InvariantCulture), shouldQuote: true);
                    csvWriter.WriteField(dto.Value);
                    csvWriter.NextRecord();
                }
            }
        }    
    }
}