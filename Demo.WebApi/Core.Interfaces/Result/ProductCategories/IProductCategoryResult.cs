using System;

namespace Core.Interfaces.Result.ProductCategories
{
    public interface IProductCategoryResult
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string DisplayName { get; set; }
    }
}
