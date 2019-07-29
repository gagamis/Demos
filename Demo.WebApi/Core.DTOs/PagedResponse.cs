using Core.Interfaces.Result;
using System.Collections.Generic;

namespace Core.DTOs
{
    public class PagedResponse<T> : IPagedResponse<T> where T : class
    {
        /// <summary>
        /// Total pages of items
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Current page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Current pag of items
        /// </summary>
        public List<T> Items { get; set; }
    }
}
