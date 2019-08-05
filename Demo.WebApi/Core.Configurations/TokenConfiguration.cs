namespace Core.Configurations
{
    public class TokenConfiguration
    {
        /// <summary>
        /// Secret for access token generation
        /// </summary>
        public string AccessTokenSecretKey { get; set; }

        /// <summary>
        /// Secret for refresh token generation
        /// </summary>
        public string RefreshTokenSecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        /// <summary>
        /// Access token lifetime in minutes
        /// </summary>
        public int AccessTokenLifetime { get; set; } = 90;

        /// <summary>
        /// Refresh token lifetime in minutes
        /// </summary>
        public int RefreshTokenLifetime { get; set; } = 300;

    }
}
