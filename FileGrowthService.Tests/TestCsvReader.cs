using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace FileGrowthService.Tests
{
    class TestCsvReader
    {
        private string _tempDir;

        [SetUp]
        public void Setup()
        {
            _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("D"));
            Directory.CreateDirectory(_tempDir);
        }

        private Mock<IConfiguration> MockConfig
        {
            get
            {
                var configMock = new Mock<IConfiguration>();
                configMock.SetupGet(p => p["WorkingDirectory"]).Returns(_tempDir);
                return configMock;
            }
        }

        [Test]
        public void TestReading()
        {
            Assert.AreEqual(_tempDir, MockConfig.Object["WorkingDirectory"]);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(_tempDir, recursive: true);
        }
    }
}
