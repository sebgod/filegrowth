namespace FileGrowthService
{
    /// <summary>
    /// Immutable struct that holds meta information about a file ID.
    /// </summary>
    public struct FileMetaData
    {
        public FileMetaData(int fileID, string filePath)
        {
            FileID = fileID;
            FilePath = filePath;
        }

        public int FileID { get; }

        public string FilePath { get; }
    }
}