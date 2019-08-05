using Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        int RefreshTokenLifetime { get; }

        string GenerateRefreshToken(User user);
        string GenerateAccessToken(User user);
        void RemoveOldAndExpiredTokens(User user, string token);
        string Encode(string str);
        string Decode(string str);
        string ValidateToken(User user, HttpContext httpContext);
    }

}
