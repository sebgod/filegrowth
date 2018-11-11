using System.IO;

namespace FileGrowthService.File
{
    public class FileStreamProvider : IFileStreamProvider
    {
        public Stream OpenRead(string filePath)
            => System.IO.File.OpenRead(filePath);

        public Stream OpenWrite(string filePath)
            => new FileStream(filePath, FileMode.Truncate, FileAccess.Write, FileShare.Read, 1024 * 10);
    }
}
