using Core.Interfaces.Result.ProductCategories;
using System;

namespace Core.Interfaces.Result.Product
{
    public interface IGetProductsResult
    {
        Guid Id { get; set; }
        string Code { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
        IProductCategoryResult ProductCategory { get; set; }
    }
}
