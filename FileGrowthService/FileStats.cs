using System;
using System.Collections.Generic;

namespace FileGrowth.Services
{
    public class FileStats
    {
        private readonly SortedDictionary<DateTime, DateTime> _growthMap;

        public FileStats(int fileID)
        {
            FileID = fileID;

            _growthMap = new SortedDictionary<DateTime, DateTime>();
        }

        public int FileID { get; }

        
    }
}