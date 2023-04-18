using Domain.Entities;

namespace Domain.Services
{
    public interface ITokenGenerator
    {
        public Task<string> GenerateAccessTokenAsync(IUser user);
        public Task<string> GenerateRefreshTokenAsync(IUser user);
    }
}
