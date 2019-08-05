using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs.Users;
using Core.Entities;

namespace Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(LoginRequest requestObject);
    }
}
