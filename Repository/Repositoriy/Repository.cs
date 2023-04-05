using Database;
using Database.Entities;
using Domain.Repositories;

namespace Repository.Repositoriy
{
    public class Repository : IRepository
    {
        public readonly AppDbContext _dbContext;
        
        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<History> GetHistory()
        {
            return null;
        }

        public async Task<Topic> AddTopic()
        {
            return null;
        }   
    }
}
