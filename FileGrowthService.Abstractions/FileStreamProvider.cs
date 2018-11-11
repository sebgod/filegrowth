using System.IO;

namespace FileGrowthService
{
    public interface IFileStreamProvider
    {
        /// <summary>
        /// Opens the file for reading, behaves like <see cref="File.OpenRead(string)"/>
        /// </summary>
        /// <param name="filePath">Absolute path to the file</param>
        /// <returns>Opened stream for reading</returns>
        Stream OpenRead(string filePath);

        /// <summary>
        /// Opens the file at the given path, truncating existing data if present.
        /// </summary>
        /// <param name="filePath">Absolute path to the file</param>
        /// <returns>Opened stream for writing</returns>
        Stream OpenWrite(string filePath);
    }
}
