using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        public Task AddRefreshTokenAsync(long userId, string token, DateTime expires);
        public Task<IRefreshToken> GetRefreshTokenAsync(string token);
        public Task DeleteRefreshTokenAsync(string token);
        public Task DeleteRefreshTokensAsync(long userId);
    }
}
