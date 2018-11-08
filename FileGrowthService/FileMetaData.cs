namespace FileGrowth.Services
{
    public class FileMetaData
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