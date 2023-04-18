using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<IUser[]> GetAllsync();
        public Task AddAsync(IUser user);
        public Task DeleteAsync(long id);
        public Task UpdateAsync(long id, IUser user);
        public Task<IUser> GetByIdAsync(long id);
        public Task<IUser> GetUserByEmail(string email);
    }
}
