using NUnit.Framework;
using System;
using System.Collections.Generic;
using static FileGrowthService.FileGrowthMeasureProcessor;

namespace FileGrowthService.Tests
{
    class TestMeasuring
    {
        [TestCase(TimeSpan.TicksPerHour, 1000, ExpectedResult = 1000.0)]
        [TestCase(TimeSpan.TicksPerSecond, 0, ExpectedResult = 0)]
        [TestCase(TimeSpan.TicksPerHour / 2, 1000, ExpectedResult = 2000.0)]
        [TestCase(TimeSpan.TicksPerHour * 2, 1000, ExpectedResult = 500.0)]
        [TestCase(TimeSpan.TicksPerHour, -100, ExpectedResult = -100.0)]
        [TestCase(TimeSpan.TicksPerMinute, 60, ExpectedResult = 3600.0)]
        [TestCase(TimeSpan.TicksPerHour * 3, 125, ExpectedResult = 41d + 2d / 3d)]
        public double TestCalcGrowthRate(long deltaTimeTicks, long deltaSize)
        {
            return CalcGrowthRate(TimeSpan.FromTicks(deltaTimeTicks), deltaSize);
        }

        [TestCaseSource(nameof(FileGrowthFacts))]
        public void TestProcessGrowth(object[] timeAndSizes)
        {
            var growth = new Dictionary<DateTime, double>();
            var value = (timestamp: null as DateTime?, size: 0L, growth);
            var count = timeAndSizes.Length;

            for (var i = 0; i < count; i++)
            {
                // initialise
                var timeAndSize = (object[])timeAndSizes[i];
                var fact = new KeyValuePair<DateTime, long>((DateTime)timeAndSize[0], (long)timeAndSize[1]);
                var deltaSize = timeAndSize[2] as double?;

                // process
                value = ProcessGrowth(value, fact);

                // validate
                (var currentTimestamp, var currentSize, var currentGrowthMap) = value;

                Assert.AreSame(growth, currentGrowthMap, "Growth map should refer to the same object");
                Assert.AreEqual(fact.Key, currentTimestamp, "Ensure timestamp is equal");
                Assert.AreEqual(fact.Value, currentSize, "Ensure that file size is equal");

                if (deltaSize.HasValue)
                {
                    var currentGrowthRate = currentGrowthMap[fact.Key];
                    Assert.AreEqual(deltaSize.Value, currentGrowthRate, 0.1, "Delta of calculated growth rate should be within [-0.1, 0.1] of actual growth rate");
                }
                else
                {
                    Assert.False(currentGrowthMap.ContainsKey(fact.Key), "Ensure that the initial fact is not in the growth set");
                }
            }
        }

        static readonly object[] FileGrowthFacts = new object[]
        {
            new object[]
            {
                new object[]
                {
                    new DateTime(2015, 3, 25, 23, 0, 16, 902),
                    4245143L,
                    null as double?
                },
                new object[]
                {
                    new DateTime(2015, 3, 25, 23, 55, 45, 787),
                    4276852L,
                    34291.5
                },
                new object[]
                {
                    new DateTime(2015, 3, 26, 00, 56, 49, 909),
                    4308267L,
                    30865.2
                }
            },
            new object[]
            {
                new object[]
                {
                    new DateTime(2015, 3, 25, 22, 59, 03, 931),
                    7655472L,
                    null as double?
                },
                new object[]
                {
                    new DateTime(2015, 3, 25, 23, 54, 26, 892),
                    7719957L,
                    69861.2
                },
                new object[]
                {
                    new DateTime(2015, 3, 26, 1, 6, 0, 120),
                    7779949L,
                    50305.1
                }
            }
        };
    }
}
