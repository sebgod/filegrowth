using NUnit.Framework;
using System;
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
        [TestCase(TimeSpan.TicksPerHour * 3, 125, ExpectedResult = 41d + 2d/3d)]
        public double TestCalcGrowthRate(long deltaTimeTicks, long deltaSize)
        {
            return CalcGrowthRate(TimeSpan.FromTicks(deltaTimeTicks), deltaSize);
        }
    }
}
