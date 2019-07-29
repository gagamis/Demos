using Core.Entities;
using Core.Interfaces.Repository.Products;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Products
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ShopContext _model => Context as ShopContext;
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public async Task<Product> GetproductByIdAsync(Guid value) => await Find(x => x.Id == value).Include(x => x.ProductCategory).FirstOrDefaultAsync();
        
    }
}
