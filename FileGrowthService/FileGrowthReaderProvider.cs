using System;
using System.Collections.Generic;

namespace FileGrowthService
{
    /// <summary>
    /// The <see cref="IFileGrowthReaderProvider"/> provides an abstraction for retrieving the raw
    /// file size facts per file ID and point in time (timestamp).
    /// </summary>
    public interface IFileGrowthReaderProvider
    {
        IReadOnlyDictionary<int, FileMetaData> FileMap { get; }

        IReadOnlyDictionary<int, FileSizeStats> FileSizeStatsMap { get; }
    }
}