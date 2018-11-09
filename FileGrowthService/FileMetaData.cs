namespace FileGrowthService
{
    /// <summary>
    /// Immutable struct that holds meta information about a file ID.
    /// </summary>
    public struct FileMetaData
    {
        public FileMetaData(int fileID, string name)
        {
            FileID = fileID;
            Name = name;
        }

        public int FileID { get; }

        public string Name { get; }
    }
}