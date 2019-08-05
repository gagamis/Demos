using Core.Configurations;
using Core.Entities;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Tokens
{
    public class TokenService : ITokenService
    {
        public int RefreshTokenLifetime { get { return _tokenConfiguration.RefreshTokenLifetime; } }
        private readonly TokenConfiguration _tokenConfiguration;

        public TokenService(IOptions<TokenConfiguration> tokenConfig)
        {
            _tokenConfiguration = tokenConfig.Value;
        }

        /// <summary>
        /// Generate RefreshToken
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>RefreshToken</returns>
        public string GenerateRefreshToken(User userName) => GenerateToken(userName, _tokenConfiguration.RefreshTokenSecretKey, _tokenConfiguration.RefreshTokenLifetime);

        /// <summary>
        /// Generate AccessToken
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>AccessToken</returns>
        public string GenerateAccessToken(User userName) => GenerateToken(userName, _tokenConfiguration.AccessTokenSecretKey, _tokenConfiguration.AccessTokenLifetime);
        /// <summary>
        /// Remove user's expired tokens
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="token"></param>
        public void RemoveOldAndExpiredTokens(User userName, string token)
        {
            // Remove old token
            userName.Tokens.Remove(userName.Tokens.FirstOrDefault(x => x.Token == token));

            // Remove expired user tokens
            ((HashSet<UserToken>)userName.Tokens).RemoveWhere(x => x.ValidFor < DateTime.UtcNow);

        }
        public string Encode(string str) => Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        public string Decode(string str) => Encoding.UTF8.GetString(Convert.FromBase64String(str));

        /// <summary>
        /// Validate user's token
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="httpContext">Current HttpContext</param>
        /// <returns></returns>
        public string ValidateToken(User userName, HttpContext httpContext)
        {
            string token = null;

            // Get the token
            if (string.IsNullOrEmpty(token = GetTokenFromBearer(httpContext)))
                return null;

            // Validate token
            if (!HasUsersRefreshToken(userName, token))
                return null;
            else
                return token;

        }


        // Private
        private string GetTokenFromBearer(HttpContext httpContext)
        {
            string bearerToken = httpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(bearerToken) || !bearerToken.StartsWith("Bearer "))
                return null;

            string token = bearerToken.Substring("Bearer ".Count());

            if (string.IsNullOrEmpty(token))
                return null;

            return token;
        }
        private bool HasUsersRefreshToken(User userName, string refreshToken) => userName.Tokens.Any(x => x.Token == refreshToken);
        private string GenerateToken(User user, string secretKey, int expire)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, Encode(user.Id.ToString())),
                }),
                IssuedAt = DateTime.UtcNow,
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                Expires = DateTime.UtcNow.AddMinutes(expire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
