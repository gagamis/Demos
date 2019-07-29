using Core.Interfaces.Repository.Products;
using Core.Interfaces.Repository.Users;
using System.Threading.Tasks;

namespace Core.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }

        Task<int> SaveAsync();
    }
}
