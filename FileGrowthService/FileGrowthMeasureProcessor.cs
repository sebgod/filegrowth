using System;
using System.Collections.Generic;
using System.Linq;

namespace FileGrowthService
{
    public class FileGrowthMeasureProcessor : IFileGrowthMeasureProcessor
    {
        public FileGrowthStats ProcessFile(FileSizeStats stats)
        {
            return stats.Aggregate(
                (
                    Timestamp: null as DateTime?,
                    Size: 0L,
                    Growth: new Dictionary<DateTime, double>()
                ), 
                (pLast, pCurrent) =>
                    {
                        var newTimestamp = pCurrent.Key;
                        var newSize = pCurrent.Value;
                        if (pLast.Timestamp != null)
                        {
                            pLast.Growth[newTimestamp] =
                                CalcGrowthRate(newTimestamp - pLast.Timestamp.Value, newSize - pLast.Size);
                        }
                        return (newTimestamp, newSize, pLast.Growth);
                    },
                p => new FileGrowthStats(stats.FileID, p.Growth)
            );
        }

        private static readonly double MSPerHour = (double) TimeSpan.FromHours(1).TotalMilliseconds;

        public static double CalcGrowthRate(TimeSpan deltaTime, long deltaDiff)
        {
            return deltaDiff * MSPerHour / deltaTime.TotalMilliseconds;
        }
    }
}