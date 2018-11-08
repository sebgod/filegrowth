using System;
using System.Collections.Generic;

namespace FileGrowth.Services
{
    public interface IFileGrowthService
    {
        void UpdateFileList(IEnumerable<FileMetaData> fileList);

        FileMetaData GetFileMetaData(int fileID);

        void UpdateFileStats(IEnumerable<FileStats> fileStats);
    }
}