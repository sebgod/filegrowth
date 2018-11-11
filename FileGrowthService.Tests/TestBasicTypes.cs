using NUnit.Framework;
using System;

namespace FileGrowthService.Tests
{
    class TestBasicTypes
    {
        [TestCaseSource(nameof(EqualityPatterns))]
        public void TestEquality(object a, object b, bool equal)
        {
            Assert.AreEqual(equal, a?.Equals(b) ?? ReferenceEquals(b, null));
            Assert.AreEqual(equal, a?.GetHashCode() == b?.GetHashCode());
        }

        static readonly object[] EqualityPatterns =
            new object[]
            {
                new object[] { new FileMetaData(1, "test"), new FileMetaData(1, "test2"), false },
                new object[] { new FileMetaData(1, "test"), new FileMetaData(2, "test"), false },
                new object[] { new FileMetaData(1, "test"), new FileMetaData(1, "test"), true },
                new object[] { new FileMetaData(1, "test"), null, false },
                new object[] { new FileMetaData(1, "test"), new object(), false },
                new object[] { new FileMetaData(1, "test"), new DateTime(2017, 1, 1), false },
                new object[] { new object(), new FileMetaData(1, "test"), false },
                new object[] { null, new FileMetaData(1, "test"), false },
                new object[] { null, null, true }
            };
    }
}
