
using Domain.Entities;

namespace Domain.Services
{
    public interface IChatService
    {
        public Task<string> GetResponse(long userId, string messageText);
        public Task<List<ITopic>> GetTopics(long userId);
        public Task ClearTopics(long userId);
    }
}
