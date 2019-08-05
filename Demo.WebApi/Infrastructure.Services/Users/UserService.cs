using Core.DTOs.Users;
using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public UserService(IUnitOfWork uow, ITokenService tokenService)
        {
            this._unitOfWork = uow;
            this._tokenService = tokenService;
        }

        public async Task<User> ValidateUserAsync(LoginRequest rqst) => await _unitOfWork.Users.Find(x => x.UserName == rqst.UserName && x.Password == rqst.Password).SingleOrDefaultAsync();

        /// <summary>
        /// Renew token
        /// </summary>
        public async Task<LoginResult> RenewTokenAsync(TokenRefreshRequest rqst, HttpContext httpContext)
        {
            try
            {
                string refreshToken = null;
                string newRefreshToken = null;
                string accessToken = null;
                User user = null;

                // Fetch user
                if ((user = await _unitOfWork.Users.Find(x => x.Id == new Guid(_tokenService.Decode(rqst.Id))).Include(x => x.Tokens).FirstOrDefaultAsync()) == null)
                    return null;

                // Get and validate token
                if (string.IsNullOrEmpty(refreshToken = _tokenService.ValidateToken(user, httpContext)))
                    return null;

                // Remove old and expired tokens
                _tokenService.RemoveOldAndExpiredTokens(user, refreshToken);

                // Generat tokens
                newRefreshToken = _tokenService.GenerateRefreshToken(user);
                accessToken = _tokenService.GenerateAccessToken(user);

                // Persisting new refreshToken
                user.Tokens.Add(new UserToken() { UserId = user.Id, Token = newRefreshToken, ValidFor = DateTime.UtcNow.AddMinutes(_tokenService.RefreshTokenLifetime) });
                // user.LastLogIn = DateTime.UtcNow;
                await _unitOfWork.SaveAsync();

                // Save token into Redis


                return new LoginResult()
                {
                    TokenType = "bearer",
                    Id = _tokenService.Encode(user.Id.ToString()),
                    RefreshToken = newRefreshToken,
                    AccessToken = accessToken,
                    Expires = _tokenService.RefreshTokenLifetime
                };

            }
            catch (Exception ex)
            {
                throw new Exception("[UserService]: Failed to renew tokens", ex);
            }
        }

    }
}
