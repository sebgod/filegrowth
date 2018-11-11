using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileGrowthService.Tests
{
    abstract class TestCsvBase
    {
        [SetUp]
        public void Setup()
        {
            WorkingDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("D"));
            Directory.CreateDirectory(WorkingDirectory);
            WriteSourceFiles();
        }

        protected string WorkingDirectory { get; private set; }

        protected IConfiguration MockConfig
        {
            get
            {
                var configMock = new Mock<IConfiguration>();
                configMock.SetupGet(p => p[nameof(WorkingDirectory)]).Returns(WorkingDirectory);
                configMock.SetupGet(p => p[nameof(FileIDName)]).Returns(FileIDName);
                configMock.SetupGet(p => p[nameof(FileStatsName)]).Returns(FileStatsName);
                return configMock.Object;
            }
        }

        [Test]
        public void TestMockWorkingDirectory()
        {
            Assert.AreEqual(WorkingDirectory, MockConfig[nameof(WorkingDirectory)]);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(WorkingDirectory, recursive: true);
        }

        protected void WriteSourceFiles()
        {
            var utf8 = new UTF8Encoding(false);
            foreach (var sourceFile in SourceFiles)
            {
                System.IO.File.WriteAllText(Path.Combine(WorkingDirectory, sourceFile.Key), sourceFile.Value, utf8);
            }
        }

        const string FileIDName = "Files.csv";
        const string FileStatsName = "FileStats.csv";

        protected static readonly IReadOnlyDictionary<string, string> SourceFiles =
            new Dictionary<string, string>()
            {
                [FileIDName] = @"""ID"",""Name""
1,""1.mdf""
",
                [FileStatsName] = @"""FileID"",""Timestamp"",""SizeInBytes""
1,""2015-03-25 23:00:16.902"",4245143
1,""2015-03-25 23:55:45.787"",4276852
1,""2015-03-26 00:56:49.909"",4308267
1,""2015-03-26 01:59:24.107"",4366566
"
            };

        protected static readonly object[] FactTable = new object[]
        {
            (
                new FileMetaData(1, "1.mdf"),
                new FileSizeStats(
                    1,
                    new Dictionary<DateTime, long>()
                    {
                        [new DateTime(2015, 3, 25, 23, 0, 16, 902)] = 4245143L,
                        [new DateTime(2015, 3, 25, 23, 55, 45, 787)] = 4276852L,
                        [new DateTime(2015, 3, 26, 0, 56, 49, 909)] = 4308267L,
                        [new DateTime(2015, 3, 26, 1, 59, 24, 107)] = 4366566L
                    }
                ),
                new FileGrowthStats(
                    1,
                    new Dictionary<DateTime, double>()
                    {
                        [new DateTime(2015, 3, 25, 23, 55, 45, 787)] = 34291.5,
                        [new DateTime(2015, 3, 26, 0, 56, 49, 909)] = 30865.2,
                        [new DateTime(2015, 3, 26, 1, 59, 24, 107)] = 55904.5
                    }
                )
            )
        };

        protected static readonly IReadOnlyDictionary<string, string> OutputFiles =
            new Dictionary<string, string>()
            {
                ["1.csv"] = @"""FileID"",""Name"",""Timestamp"",""SizeInBytes"",""GrowthRatesInBytesPerHour""
1,""1.mdf"",""2015-03-25 23:55:45.787"",4276852,34291.5
1,""1.mdf"",""2015-03-26 00:56:49.909"",4308267,30865.2
1,""1.mdf"",""2015-03-26 01:59:24.107"",4366566,55904.5
"
            };
    }
}
