
using Core.Interfaces.Repository;
using Core.Interfaces.Repository.Products;
using Core.Interfaces.Repository.Users;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Products;
using Infrastructure.Repositories.Users;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork  : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public IProductRepository Products { get; set; }

        private ShopContext _context;

        public UnitOfWork(ShopContext context)
        {
            this._context = context;

            this.Users = new UserRepository(context);
            this.Products = new ProductRepository(context);
        }

        /// <summary>
        /// Save transaction
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}
