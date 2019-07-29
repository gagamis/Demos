using System.Collections.Generic;

namespace Core.Interfaces.Result
{
    public interface IPagedResponse<T> where T : class
    {
        /// <summary>
        /// Total items number
        /// </summary>
        int TotalPages { get; set; }

        /// <summary>
        /// Items in required page
        /// </summary>
        List<T> Items { get; set; }
    }
}
