using FileGrowthService.Csv;
using FileGrowthService.File;
using NUnit.Framework;
using System.Linq;

namespace FileGrowthService.Tests
{
    class TestCsvWriter : TestCsvBase
    {
        [TestCaseSource(nameof(FactTable))]
        public void TestWritingToCsvFile(object facts)
        {
            (var metaData, var fileSizeStats, var fileGrowthStats) = ((FileMetaData, FileSizeStats, FileGrowthStats))facts;

            IFileGrowthWriterProvider writer = new FileGrowthCsvWriterProvider(MockConfig, new FileStreamProvider());

            writer.WriteDenormalisedFileGrowthStats(metaData, fileSizeStats, fileGrowthStats);
        }
    }
}