using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITopicRepository
    {
        public Task<ITopic[]> GetAllAsync();
        public Task AddAsync(ITopic topic);
        public Task DeleteAsync(long id);
        public Task UpdateAsync(ITopic topic);
    }
}
