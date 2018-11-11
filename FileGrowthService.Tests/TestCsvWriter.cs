using FileGrowthService.Csv;
using FileGrowthService.File;
using NUnit.Framework;
using System.IO;
using System.Text;

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

            var outPath = Path.Combine(WorkingDirectory, File1OutName);
            var actualOutput = System.IO.File.ReadAllText(outPath, new UTF8Encoding(false));

            Assert.That(actualOutput, Is.EqualTo(OutputFiles[File1OutName]));
        }
    }
}