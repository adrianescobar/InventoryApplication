namespace InventoryApplication.Domain.Repository.Base
{
    public class Paged<T>
    {
        /// <summary>
        /// The items for the current page
        /// </summary>
        public IEnumerable<T> Items { get; set; }
        /// <summary>
        ///  Current page number
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// Total number of items across all pages
        /// </summary>
        public int TotalCount { get; set; }                
        /// <summary>
        /// Total number of pages
        /// </summary>        
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public Paged(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            Items = items;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }

}
