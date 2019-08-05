using Core.DTOs.Products;
using Core.Interfaces.Result;
using Core.Interfaces.Result.Product;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Shop.WebApi.Controllers
{
    [Authorize(Roles = "User")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            this._productService = productService;
            this._logger = logger;
        }

        /// <summary>
        /// Get products
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <param name="queryObject">Query object</param>
        /// <returns>Filtered, sorted and paged products</returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IPagedResponse<IGetProductsResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductsAsync([FromQuery]Guid? productId, [FromQuery]GetProductsRequest queryObject)
        {
            try
            {
                return Ok(await _productService.GetProductsAsync(productId, queryObject));
            }
            catch (Exception)
            {
                throw; // catch by ApiExceptionFilter

                //////_logger.LogError("Failed to get products", ex);
                //////return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="queryObject"></param>
        /// <returns>Id of added product</returns>
        [HttpPost()]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddProductAsync([FromBody]AddProductRequest queryObject)
        {
            try
            {
                return Ok(await _productService.AddProductAsync(queryObject));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to add new product", ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Remove an product by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>IsSuccess</returns>
        [HttpDelete()]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveProductAsync([FromQuery]Guid? productId)
        {
            try
            {
                if (!productId.HasValue)
                    throw new ArgumentNullException(nameof(productId));

                return Ok(await _productService.RemoveProductAsync(productId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete product", ex);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}