using System;

namespace FileGrowthService.Csv
{
    /// <summary>
    /// Interanl DTO for parsing the file stats CSV
    /// </summary>
    internal class FileSizeStatsDto
    {
        public int FileID { get; set; }
        public DateTime Timestamp { get; set; }
        public long SizeInBytes { get; set; }
    }
}