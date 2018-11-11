using FileGrowthService.Csv;
using FileGrowthService.File;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileGrowthService.Tests
{
    class TestCsvReader : TestCsvBase
    {
        [TestCaseSource(nameof(SourceFacts))]
        public void TestReadingCsvFromFile(object fact)
        {
            (var metaData, var fileStats) = ((FileMetaData, FileSizeStats))fact;

            IFileGrowthReaderProvider reader = new FileGrowthCsvReaderProvider(MockConfig, new FileStreamProvider());

            Assert.That(reader.FileMap.Count, Is.EqualTo(1), "We expect one file during testing");
            Assert.That(reader.FileMap.Values.Single(), Is.EqualTo(metaData), "Verify that file table is identical");
            Assert.That(reader.FileSizeStatsMap.Values.Single(), Is.EqualTo(fileStats), "Verify that file table is identical");
        }
    }
}
