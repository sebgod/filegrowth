using System;
using System.Collections.Generic;
using System.Linq;

namespace FileGrowthService
{
    using GrowthMap = Dictionary<DateTime, double>;

    public class FileGrowthMeasureProcessor : IFileGrowthMeasureProcessor
    {
        public FileGrowthStats ProcessFile(FileSizeStats stats)
        {
            return stats.Aggregate(
                (
                    timestamp: null as DateTime?,
                    size: 0L,
                    growth: new GrowthMap()
                ), 
                ProcessGrowth,
                p => new FileGrowthStats(stats.FileID, p.growth)
            );
        }

        /// <summary>
        /// Does the actual measurement of file growth by using a lookbehind of one point in time.
        /// Growth rate is expressed in bytes per hour, thus extrapolated from the delta of the last and current measurement of actual file size.
        /// </summary>
        /// <param name="last">tuple of last recorded time stamp (is null for first record),
        /// file size, and a reference to the mapping of all file growth deltas per timestamp</param>
        /// <param name="current">current measurement of file size at a point in time</param>
        /// <returns>tuple with aggregated measurement information</returns>
        public static (DateTime? timestamp, long size, GrowthMap growth) ProcessGrowth(
            (DateTime? timestamp, long size, GrowthMap growth) last,
            KeyValuePair<DateTime, long> current)
        {
            var newTimestamp = current.Key;
            var newSize = current.Value;
            (var lastTimestamp, var lastSize, var growthMap) = last;

            if (lastTimestamp != null)
            {
                var deltaTime = newTimestamp - lastTimestamp.Value;
                var deltaSize = newSize - lastSize;
                growthMap[newTimestamp] = CalcGrowthRate(deltaTime, deltaSize);
            }
            return (newTimestamp, newSize, growthMap);
        }

        private static readonly double MSPerHour =
            TimeSpan.FromHours(1).TotalMilliseconds;

        public static double CalcGrowthRate(TimeSpan deltaTime, long deltaSize)
            => deltaSize * MSPerHour / deltaTime.TotalMilliseconds;
    }
}