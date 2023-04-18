using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.IdentityModel.Tokens;
using Services.ServiceModels;

namespace Services.Services
{
    public class TokenGeneratorService : ITokenGenerator
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secretKey;
        private readonly int _accessExpiryInMinutes;
        private readonly int _refreshExpiryInMinutes;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public TokenGeneratorService(string issuer, string audience, string secretKey, int accessExpiryInMinutes, int refreshExpiryInMinutes, IRefreshTokenRepository refreshTokenRepository)
        {
            _issuer = issuer;
            _audience = audience;
            _secretKey = secretKey;
            _accessExpiryInMinutes = accessExpiryInMinutes;
            _refreshExpiryInMinutes = refreshExpiryInMinutes;
            _refreshTokenRepository = refreshTokenRepository;
        }

        /// <summary>
        /// Generate access token.
        /// </summary>
        public async Task<string> GenerateAccessTokenAsync(IUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            if (key.KeySize < 128/8)
            {
                throw new ArgumentOutOfRangeException("The encryption algorithm 'HS256' requires a key size of at least '128' bits.");
            }

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_accessExpiryInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Generate simple refresh token.
        /// </summary>
        public async Task<string> GenerateRefreshTokenAsync(IUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            if (key.KeySize < 128/8)
            {
                throw new ArgumentOutOfRangeException("The encryption algorithm 'HS256' requires a key size of at least '128' bits.");
            }

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_refreshExpiryInMinutes),
                signingCredentials: creds);

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = DateTime.Now.AddMinutes(_refreshExpiryInMinutes)
            };
            await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken.UserId, refreshToken.Token, refreshToken.Expires);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
