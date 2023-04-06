using Domain.Entities;

namespace Domain.Repositories
{
    public interface IHistoryRepository
    {
        public Task<IHistory[]> GetByUserIdAsync(long userId);

        public Task AddAsync(IHistory history);
        public Task DeleteAsync (long id);
    }
}
