using Core.Entities;
using Core.Interfaces.Repository.Users;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private ShopContext _model => Context as ShopContext;

        public UserRepository(DbContext model) : base(model)
        { }

    }
}
