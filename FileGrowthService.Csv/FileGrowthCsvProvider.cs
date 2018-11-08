using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace FileGrowthService.Csv
{
    public class FileGrowthCsvProvider : IFileGrowthService
    {
        public FileGrowthCsvProvider(IConfiguration configuration)
        {
            var pwd = configuration["WorkingDirectory"];
            var fileIDPath = Path.Combine(pwd, configuration["FileIDName"]);
            var fileStatsPath = Path.Combine(pwd, configuration["FileStatsName"]);
        }

        public IEnumerable<FileMetaData> FileList { get; } = null;

        public IReadOnlyDictionary<int, FileStats> FileStats { get; } = null;
    }
}
