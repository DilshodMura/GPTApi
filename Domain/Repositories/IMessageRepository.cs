using Domain.Entities;

namespace Domain.Repositories
{
    public interface IMessageRepository
    {
        public Task<IMessage[]> GetAllAsync();
        public Task AddAsync(IMessage message);
    }
}
