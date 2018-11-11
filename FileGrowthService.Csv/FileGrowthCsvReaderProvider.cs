using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Microsoft.Extensions.Configuration;

namespace FileGrowthService.Csv
{
    public class FileGrowthCsvReaderProvider : IFileGrowthReaderProvider
    {
        public FileGrowthCsvReaderProvider(IConfiguration configuration, IFileStreamProvider fileStreamProvider)
        {
            var pwd = configuration["WorkingDirectory"];
            var fileIDPath = Path.Combine(pwd, configuration["FileIDName"]);
            var fileStatsPath = Path.Combine(pwd, configuration["FileStatsName"]);
        
            FileMap = ( 
                from dto in ParseCSVFile<FileMetaDataDto>(fileStreamProvider, fileIDPath)
                select new FileMetaData(dto.ID, dto.Name)
            ).ToDictionary(p => p.FileID, p => p);
            
            FileSizeStatsMap = (
                from dto in ParseCSVFile<FileSizeStatsDto>(fileStreamProvider, fileStatsPath)
                group dto by dto.FileID into g
                select new FileSizeStats(g.Key, g.ToDictionary(p => p.Timestamp, p => p.SizeInBytes))
            ).ToDictionary(p => p.FileID, p => p);
        }

        private IList<T> ParseCSVFile<T>(IFileStreamProvider fileStreamProvider, string filePath)
            where T : class, new()
        {
            using (var fileStream = fileStreamProvider.OpenRead(filePath))
            using (var textReader = new StreamReader(
                fileStream,
                new UTF8Encoding(false),
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 10 * 1024,
                leaveOpen: true))
            using (var csvReader = new CsvReader(textReader, leaveOpen: true))
            {
                csvReader.Configuration.RegisterClassMap(csvReader.Configuration.AutoMap<T>());
                return csvReader.GetRecords<T>().ToList();
            }
        }

        public IReadOnlyDictionary<int, FileMetaData> FileMap { get; }

        public IReadOnlyDictionary<int, FileSizeStats> FileSizeStatsMap { get; }
    }
}
