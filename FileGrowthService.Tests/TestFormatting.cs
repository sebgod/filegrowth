using FileGrowthService.Csv;
using NUnit.Framework;
using System;

namespace FileGrowthService.Tests
{
    public class TestFormatting
    {
        [TestCase(123.349, ExpectedResult = "123.3")]
        [TestCase(1d, ExpectedResult = "1.0")]
        [TestCase(1.5, ExpectedResult = "1.5")]
        public string TestFormatHourlyGrowthRate(double rate)
        {
            return FileGrowthCsvWriterProvider.FormatHourlyGrowthRate(rate);
        }

        public void TestFormatHourlyGrowthRateExact()
        {
            var result = FileGrowthCsvWriterProvider.FormatTime(new DateTime(2015, 3, 25, 23, 0, 16, 902));
            Assert.AreEqual("2015-03-25 23:00:16.902", result);
        }

        [Test]
        public void TestFormatHourlyGrowthFuzzy()
        {
            var result = FileGrowthCsvWriterProvider.FormatTime(new DateTime(2015, 3, 25));
            Assert.AreEqual("2015-03-25 00:00:00.000", result);
        }
    }
}