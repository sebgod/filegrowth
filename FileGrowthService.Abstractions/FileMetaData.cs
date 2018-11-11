using System;

namespace FileGrowthService
{
    /// <summary>
    /// Immutable struct that holds meta information about a file ID.
    /// </summary>
    public struct FileMetaData : IEquatable<FileMetaData>
    {
        public FileMetaData(int fileID, string name)
        {
            FileID = fileID;
            Name = name;
        }

        public int FileID { get; }

        public string Name { get; }

        public bool Equals(FileMetaData other)
            => FileID == other.FileID && Name == other.Name;

        public override bool Equals(object obj)
            => !ReferenceEquals(obj, null) && obj is FileMetaData ? Equals((FileMetaData)obj) : false;

        public override int GetHashCode()
        {
            unchecked { return 7979 * FileID ^ Name.GetHashCode(); };
        }
    }
}