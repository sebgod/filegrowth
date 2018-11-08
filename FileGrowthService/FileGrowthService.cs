using System;
using System.Collections.Generic;

namespace FileGrowthService
{
    public interface IFileGrowthService
    {
        IEnumerable<FileMetaData> FileList { get; }

        IReadOnlyDictionary<int, FileStats> FileStats { get; }
    }
}