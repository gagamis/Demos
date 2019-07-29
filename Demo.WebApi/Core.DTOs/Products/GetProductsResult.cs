using Core.Interfaces.Result.Product;
using Core.Interfaces.Result.ProductCategories;
using System;

namespace Core.DTOs.Products
{
    public class GetProductResult : IGetProductsResult
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public IProductCategoryResult ProductCategory { get; set; }
    }
}
