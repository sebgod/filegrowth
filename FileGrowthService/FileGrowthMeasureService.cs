using System.Linq;

namespace FileGrowthService
{
    public sealed class FileGrowthMeasureService : IFileGrowthMeasureService
    {
        public FileGrowthMeasureService(
            IFileGrowthReaderProvider reader,
            IFileGrowthWriterProvider writer,
            IFileGrowthMeasureProcessor processor)
        {
            Reader = reader;
            Writer = writer;
            Processor = processor;
        }

        public IFileGrowthReaderProvider Reader { get; }
        public IFileGrowthWriterProvider Writer { get; }
        public IFileGrowthMeasureProcessor Processor { get; }

        public void ProcessFiles()
        {
            foreach (var kv in Reader.FileMap.OrderBy(p => p.Key))
            {
                var fileSizeStats = Reader.FileSizeStatsMap[kv.Key];
                var fileProcessedStats = Processor.ProcessFile(fileSizeStats);
                Writer.WriteDenormalisedFileGrowthStats(kv.Value, fileSizeStats, fileProcessedStats);
            }
        }
    }
}