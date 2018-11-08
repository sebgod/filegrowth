using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using FileGrowthService;
using Microsoft.Extensions.Configuration;

namespace FileGrowthService.Csv
{
    public class FileGrowthCsvProvider : IFileGrowthService
    {
        /// <summary>
        /// Interanl DTO for parsing the file meta data CSV
        /// </summary>
        class FileMetaDataDto
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// Interanl DTO for parsing the file stats CSV
        /// </summary>
        class FileStatsDto
        {
            public int FileID { get; set; }
            public DateTime Timestamp { get; set; }
            public long SizeInBytes { get; set; }
        }

        public FileGrowthCsvProvider(IConfiguration configuration)
        {
            var pwd = configuration["WorkingDirectory"];
            var fileIDPath = Path.Combine(pwd, configuration["FileIDName"]);
            var fileStatsPath = Path.Combine(pwd, configuration["FileStatsName"]);
        
            FileList = ParseCSVFile<FileMetaDataDto>(fileIDPath)
                .Select(p => new FileMetaData(p.ID, p.Name))
                .ToList();
            
            FileStats = (
                from dto in ParseCSVFile<FileStatsDto>(fileStatsPath)
                group dto by dto.FileID into g
                select new FileStats(g.Key, g.ToDictionary(p => p.Timestamp, p => p.SizeInBytes))
            ).ToDictionary(p => p.FileID, p => p);
        }

        private IList<T> ParseCSVFile<T>(string filePath)
            where T : class, new()
        {
            var utf8 =  new UTF8Encoding(false);

            using (var textReader = new StreamReader(File.OpenRead(filePath), utf8))
            using (var csvReader = new CsvReader(textReader, leaveOpen: true))
            {
                csvReader.Configuration.RegisterClassMap(csvReader.Configuration.AutoMap<T>());
                return csvReader.GetRecords<T>().ToList();
            }
        }

        public IEnumerable<FileMetaData> FileList { get; }

        public IReadOnlyDictionary<int, FileStats> FileStats { get; }
    }
}
