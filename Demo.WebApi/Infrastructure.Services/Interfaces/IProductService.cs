using Core.DTOs.Products;
using Core.Interfaces.Result;
using Core.Interfaces.Result.Product;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Get products by query filters
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="queryObject"></param>
        /// <returns>paged products</returns>
        Task<IPagedResponse<IGetProductsResult>> GetProductsAsync(Guid? productId, GetProductsRequest queryObject);

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="queryObject"></param>
        /// <returns>Id of new product</returns>
        Task<Guid> AddProductAsync(AddProductRequest queryObject);

        /// <summary>
        /// Remove product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>IsSuccess</returns>
        Task<bool> RemoveProductAsync(Guid? productId);
    }
}
