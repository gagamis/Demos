using AutoMapper;
using Core.DTOs;
using Core.DTOs.Products;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Result;
using Core.Interfaces.Result.Product;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly Dictionary<string, Expression<Func<Product, string>>> _getProductsFilteringPredicates = new Dictionary<string, Expression<Func<Product, string>>>()
        {
            { "code", x => x.Code },
            { "name", x => x.DisplayName }
        };

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }


        public async Task<IPagedResponse<IGetProductsResult>> GetProductsAsync(Guid? productId, GetProductsRequest queryObject)
        {
            IQueryable<Product> products = _unitOfWork.Products.GetAll();

            // Filtering by Id
            if (productId.HasValue)
                products = products.Where(x => x.Id == productId);

            // Filtering by Code
            if (!string.IsNullOrEmpty(queryObject.Code))
                products = products.Where(x => x.Code.ToLower() == queryObject.Code.ToLower());

            // Filtering by DisplayName
            if (!string.IsNullOrEmpty(queryObject.DisplayName))
                products = products.Where(x => x.DisplayName.ToLower().Contains(queryObject.DisplayName.ToLower()));

            // Ordering
            Expression<Func<Product, string>> predicate = _getProductsFilteringPredicates["code"]; // default orderby
            if (!string.IsNullOrEmpty(queryObject.OrderBy) && _getProductsFilteringPredicates.ContainsKey(queryObject.OrderBy.ToLower()))
                predicate = _getProductsFilteringPredicates[queryObject.OrderBy.ToLower()];

            if (queryObject.IsDescending)
                products = products.OrderByDescending(predicate);
            else
                products = products.OrderBy(predicate);

            // Paging
            List<Product> queryResult = await products.Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize).Include(x => x.ProductCategory).ToListAsync();

            int rowCount = await products.CountAsync();
            double pageCount = (double)rowCount / queryObject.PageSize;
            int totalPages = (int)Math.Ceiling(pageCount);

            return new PagedResponse<IGetProductsResult>()
            {
                CurrentPage = queryObject.Page,
                TotalPages = totalPages,
                Items = _mapper.Map<List<Product>, List<IGetProductsResult>>(queryResult)
            };
        }

        public async Task<Guid> AddProductAsync(AddProductRequest queryObject)
        {
            Product newProduct = null;
            _unitOfWork.Products.Add(newProduct = _mapper.Map<AddProductRequest, Product>(queryObject));

            if (await _unitOfWork.SaveAsync() != 1)
                throw new Exception("AddProductAsync invalid response!");

            return newProduct.Id;
        }

        public async Task<bool> RemoveProductAsync(Guid? productId)
        {
            Product p = await _unitOfWork.Products.GetAsync(productId.Value);

            if (p == null)
                return false;

            _unitOfWork.Products.Remove(p);

            await _unitOfWork.SaveAsync();

            return true;
         }
    }
}
