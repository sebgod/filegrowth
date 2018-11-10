using System.Linq;

namespace FileGrowthService
{
    public sealed class FileGrowthMeasureService : IFileGrowthMeasureService
    {
        public FileGrowthMeasureService(
            IFileGrowthReaderProvider reader,
            IFileGrowthWriterProvider writer)
        {
            Reader = reader;
            Writer = writer;
        }

        public IFileGrowthReaderProvider Reader { get; }
        public IFileGrowthWriterProvider Writer { get; }

        
        public void ProcessFiles()
        {
            foreach (var kv in Reader.FileMap.OrderBy(p => p.Key))
            {
                var fileSizeStats = Reader.FileSizeStatsMap[kv.Key];
                Writer.WriteDenormalisedFileGrowthStats(kv.Value, fileSizeStats, null);
            }
        }
    }
}