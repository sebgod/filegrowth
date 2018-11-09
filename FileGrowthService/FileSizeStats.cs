using System;
using System.Collections.Generic;

namespace FileGrowthService
{
    public class FileSizeStats : FileStatsBase<long>
    {
        public FileSizeStats(int fileID, IDictionary<DateTime, long> fileSizes)
            : base(fileID, fileSizes)
        {
            // nothing to do
        }
    }
}