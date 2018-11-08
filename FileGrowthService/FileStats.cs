using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FileGrowthService
{
    public class FileStats : IEnumerable<KeyValuePair<DateTime, long>>
    {
        private readonly SortedDictionary<DateTime, long> _growthMap;

        public FileStats(int fileID, IDictionary<DateTime, long> growthMap)
        {
            FileID = fileID;

            _growthMap = new SortedDictionary<DateTime, long>(growthMap);
        }

        public int FileID { get; }

        public long this[DateTime value]
        {
            get { return _growthMap[value]; }
        }

        public IEnumerator<KeyValuePair<DateTime, long>> GetEnumerator() => _growthMap.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}