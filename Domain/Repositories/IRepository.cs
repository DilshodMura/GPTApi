using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRepository
    {
        public Task<IHistory> GetHistory();
    }
}
