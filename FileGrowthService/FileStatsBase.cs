using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FileGrowthService
{
    /// <summary>
    /// Immutable class that holds measurements for a specific file ID and point in time.
    /// Sorting by time (ascending) is preserved.
    /// </summary>
    public abstract class FileStatsBase<T> : IEnumerable<KeyValuePair<DateTime, T>>
    {
        private readonly SortedDictionary<DateTime, T> _statMap;

        public FileStatsBase(int fileID, IDictionary<DateTime, T> measurements)
        {
            FileID = fileID;

            _statMap = new SortedDictionary<DateTime, T>(measurements);
        }

        public int FileID { get; }

        /// <summary>
        /// Convenience index for retrieving the specific measurement at a given point in time.
        /// </summary>
        public T this[DateTime value]
        {
            get => _statMap[value];
        }

        /// <summary>
        /// Returns a *sorted* (ascending) enumeration of timestamp - file size pairs.
        /// </summary>
        public IEnumerator<KeyValuePair<DateTime, T>> GetEnumerator()
            => _statMap.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}