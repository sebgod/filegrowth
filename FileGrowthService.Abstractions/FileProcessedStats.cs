using System;
using System.Collections.Generic;

namespace FileGrowthService
{
    public class FileGrowthStats : FileStatsBase<double>
    {
        public FileGrowthStats(int fileID, IDictionary<DateTime, double> growthRates)
            : base(fileID, growthRates)
        {
            // nothing to do
        }
    }
}