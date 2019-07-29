using Core.Interfaces.Request;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Products
{
    public class GetProductsRequest : IPagedRequest, IOrderedRequest
    {
        // Filtering
        [MaxLength(256)]
        public string Code { get; set; }

        [MaxLength(512)]
        public string DisplayName { get; set; }


        // Paging
        [Range(1, 999, ErrorMessage = "PageSize can be between 1 and 999")]
        public int PageSize { get; set; } = 10;

        [Range(1, 999, ErrorMessage ="Page can be between 1 and 999")]
        public int Page { get; set; } = 1;


        // Ordering
        public string OrderBy { get; set; } = "code";
        public bool IsDescending { get; set; } = false;
    }
}
