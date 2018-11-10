using System;
using System.Collections.Generic;

namespace FileGrowthService
{
    /// <summary>
    /// The <see cref="IFileGrowthWriterProvider"/> provides an abstraction for storing the
    /// processed point in time, file size and growth rate facts.
    /// </summary>
    public interface IFileGrowthWriterProvider
    {
        void WriteDenormalisedFileGrowthStats(FileMetaData metaData, FileSizeStats fileSizeStats, FileGrowthStats FileGrowthStats);
    }
}