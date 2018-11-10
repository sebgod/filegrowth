using System.Collections.Generic;

namespace FileGrowthService
{
    public interface IFileGrowthMeasureProcessor
    {
        FileGrowthStats ProcessFile(FileSizeStats stats);
    }
}