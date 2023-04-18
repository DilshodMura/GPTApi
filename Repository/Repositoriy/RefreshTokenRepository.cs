using Database.Entities;
using Database;
using Domain.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Repository.Repositoriy
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _dbContext;

        public RefreshTokenRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Add refresh token.
        /// </summary>
        public async Task AddRefreshTokenAsync(long userId, string token, DateTime expires)
        {
            var refreshToken = new RefreshTokenDb
            {
                UserId = userId,
                Token = token,
                Expires = expires
            };

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get refresh token.
        /// </summary>
        public async Task<IRefreshToken> GetRefreshTokenAsync(string token)
        {
            var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

            // Use AutoMapper to map RefreshTokenDb to IRefreshToken
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RefreshTokenDb,IRefreshToken>();
            });
            var mapper = config.CreateMapper();
            var refreshTokenDto = mapper.Map<IRefreshToken>(refreshToken);

            return refreshTokenDto;
        }

        /// <summary>
        /// delete refresh token.
        /// </summary>
        public async Task DeleteRefreshTokenAsync(string token)
        {
            var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refreshToken != null)
            {
                _dbContext.RefreshTokens.Remove(refreshToken);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete refresh token by userId
        /// </summary>
        public async Task DeleteRefreshTokensAsync(long userId)
        {
            var refreshTokens = await _dbContext.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();
            _dbContext.RefreshTokens.RemoveRange(refreshTokens);
            await _dbContext.SaveChangesAsync();
        }
    }
}
