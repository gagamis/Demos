using System;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Repository.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetproductByIdAsync(Guid value);
    }
}
