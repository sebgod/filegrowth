using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileGrowthService.Tests
{
    class TestFileGrowthMeasureService : TestCsvBase
    {
        [TestCaseSource(nameof(FactTable))]
        public void TestServiceItself(object facts)
        {
            (var metaData, var fileSizeStats, _) = ((FileMetaData, FileSizeStats, FileGrowthStats))facts;

            var readerMock = new Mock<IFileGrowthReaderProvider>();
            readerMock
                .SetupGet(m => m.FileMap)
                .Returns(new Dictionary<int, FileMetaData>() { [metaData.FileID] = metaData });
            readerMock
                .SetupGet(m => m.FileSizeStatsMap)
                .Returns(new Dictionary<int, FileSizeStats>() { [fileSizeStats.FileID] = fileSizeStats });

            var writerMock = new Mock<IFileGrowthWriterProvider>();
            var processorMock = new Mock<IFileGrowthMeasureProcessor>();

            IFileGrowthMeasureService service =
                new FileGrowthMeasureService(readerMock.Object, writerMock.Object, processorMock.Object);

            service.ProcessFiles();

            writerMock.Verify(
                m => m.WriteDenormalisedFileGrowthStats(
                    It.Is<FileMetaData>(p => p.Equals(metaData)),
                    It.IsAny<FileSizeStats>(),
                    It.IsAny<FileGrowthStats>()
                ),
                Times.Once()
            );
            processorMock.Verify(m => m.ProcessFile(It.IsAny<FileSizeStats>()), Times.Once());
        }
    }
}
