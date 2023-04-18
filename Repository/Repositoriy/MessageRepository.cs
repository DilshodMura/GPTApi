using AutoMapper;
using Database;
using Database.Entities;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositoriy
{
    public sealed class MessageRepository : IMessageRepository
    {
        public readonly AppDbContext _dbContext;
        public readonly IMapper _mapper;
        public MessageRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Add message.
        /// </summary>
        public async Task AddAsync(IMessage message)
        {
            await _dbContext.Messages.AddAsync(_mapper.Map<MessageDb>(message));
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get all topics.
        /// </summary>
        public async Task<IMessage[]> GetAllAsync()
        {
            var messages = await _dbContext.Messages.AsNoTracking()
                .Include(m => m.Topic)
                .OrderByDescending(m => m.MessageTime)
                .ToArrayAsync();

            return messages.Select(m => _mapper.Map<IMessage>(m)).ToArray();
        }
    }
}
