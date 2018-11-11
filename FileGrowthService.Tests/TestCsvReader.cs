using FileGrowthService.Csv;
using FileGrowthService.File;
using NUnit.Framework;
using System.Linq;

namespace FileGrowthService.Tests
{
    class TestCsvReader : TestCsvBase
    {
        [TestCaseSource(nameof(FactTable))]
        public void TestReadingCsvFromFile(object facts)
        {
            (var metaData, var fileSizeStats, _) = ((FileMetaData, FileSizeStats, FileGrowthStats))facts;

            IFileGrowthReaderProvider reader = new FileGrowthCsvReaderProvider(MockConfig, new FileStreamProvider());

            Assert.That(reader.FileMap.Count, Is.EqualTo(1), "We expect one file during testing");
            Assert.That(reader.FileMap.Values.Single(), Is.EqualTo(metaData), "Verify that file table is identical");
            Assert.That(reader.FileSizeStatsMap.Values.Single(), Is.EqualTo(fileSizeStats), "Verify that file table is identical");
        }

        [TestCaseSource(nameof(FactTable))]
        public void TestReadingCsvFromString(object facts)
        {
            (var metaData, var fileSizeStats, _) = ((FileMetaData, FileSizeStats, FileGrowthStats))facts;

            IFileGrowthReaderProvider reader = new FileGrowthCsvReaderProvider(MockConfig, MockFileStreamProvider);

            Assert.That(reader.FileMap.Count, Is.EqualTo(1), "We expect one file during testing");
            Assert.That(reader.FileMap.Values.Single(), Is.EqualTo(metaData), "Verify that file table is identical");
            Assert.That(reader.FileSizeStatsMap.Values.Single(), Is.EqualTo(fileSizeStats), "Verify that file table is identical");
        }
    }
}
