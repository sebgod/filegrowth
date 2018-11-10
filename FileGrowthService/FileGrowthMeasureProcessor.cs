using System;
using System.Collections.Generic;
using System.Linq;

namespace FileGrowthService
{
    using GrowthMap = System.Collections.Generic.Dictionary<DateTime, double>;

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

        private static readonly double MSPerHour = (double) TimeSpan.FromHours(1).TotalMilliseconds;

        public static double CalcGrowthRate(TimeSpan deltaTime, long deltaDiff)
            => deltaDiff * MSPerHour / deltaTime.TotalMilliseconds;
    }
}