namespace FileGrowthService
{
    public interface IFileGrowthMeasureService
    {
        /// <summary>
        /// Reader source to read original facts from.
        /// </summary>
        IFileGrowthReaderProvider Reader { get; }

        /// <summary>
        /// Writer sink to write processed facts to.
        /// </summary>
        IFileGrowthWriterProvider Writer { get; }

        /// <summary>
        /// Processor that does the actual measurement of file growth rate.
        /// </summary>
        IFileGrowthMeasureProcessor Processor { get; }

        void ProcessFiles();
    }
}