namespace Core.Interfaces.Request
{
    public interface IPagedRequest
    {
        /// <summary>
        /// Number of items in a page
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// Requered page
        /// </summary>
        int Page { get; set; }
    }
}
