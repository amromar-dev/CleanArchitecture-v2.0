namespace CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes
{
    /// <summary>
    /// Represents a paginated result set for a query, including information about pagination.
    /// </summary>
    /// <typeparam name="T">The type of items in the result set.</typeparam>
    /// <remarks>
    /// Initializes a new instance of the <see cref="PageList{T}"/> class.
    /// </remarks>
    /// <param name="items">The list of items on the current page.</param>
    /// <param name="totalCount">The total number of items available across all pages.</param>
    /// <param name="pageNumber">The current page number (1-based index).</param>
    /// <param name="pageSize">The number of items per page.</param>
    [Serializable]
    public sealed class PageList<T>(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        /// <summary>
        /// Gets or sets the list of items on the current page.
        /// </summary>
        public List<T> Items { get; private set; } = items ?? [];

        /// <summary>
        /// Gets or sets the total number of items available across all pages.
        /// </summary>
        public int TotalCount { get; private set; } = totalCount;

        /// <summary>
        /// Gets or sets the current page number (1-based index).
        /// </summary>
        public int PageNumber { get; private set; } = pageNumber;

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; private set; } = pageSize;

        /// <summary>
        /// Gets the total number of pages based on the total item count and page size.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
