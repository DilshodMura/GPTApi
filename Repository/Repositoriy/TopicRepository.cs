using AutoMapper;
using Database;
using Database.Entities;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repository
{
    public sealed class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public TopicRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(ITopic topic)
        {
            await _dbContext.Topics.AddAsync(_mapper.Map<TopicDb>(topic));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ITopic[]> GetAllAsync()
        {
            var topics = await _dbContext.Topics
                .Select(t => _mapper.Map<ITopic>(t))
                .ToArrayAsync();

            return topics;
        }

        public async Task UpdateNameAsync(string name)
        {
            var topic = await _dbContext.Topics.FirstOrDefaultAsync(t => t.Name == name);
            if (topic != null)
            {
                topic.Name = name;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(long id)
        {
            var topic = await _dbContext.Topics.FirstOrDefaultAsync(t => t.Id == id);
            if (topic != null)
            {
                _dbContext.Topics.Remove(topic);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
