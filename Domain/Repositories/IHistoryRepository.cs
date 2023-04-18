using Domain.Entities;

namespace Domain.Repositories
{
    public interface IHistoryRepository
    {
        public Task<IHistory> GetByUserIdAsync(long userId);

        public Task AddAsync(IHistory history);
        public Task DeleteAsync (long id);
        public Task<IHistory> CreateIfNotExistsAsync(long userId);

        public Task UpdateAsync(IHistory history);
        public Task<List<ITopic>> GetAllTopicsAsync(long historyId);
    }
}
