using AutoMapper;
using Database;
using Database.Entities;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.BusinessModels;

namespace Repository.Repositoriy
{
    public class HistoryRepository : IHistoryRepository
    {
        public readonly AppDbContext _dbContext;
        public readonly IMapper _mapper;
        
        public HistoryRepository(AppDbContext dbContext, IMapper mapper)
        {
            _mapper= mapper;
            _dbContext = dbContext;
        }
        /// <summary>
        /// Add history.
        /// </summary>
        public async Task AddAsync(IHistory history)
        {
            await _dbContext.Histories.AddAsync(_mapper.Map<HistoryDb>(history));
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Delete history.
        /// </summary>
        public async Task DeleteAsync(long id)
        {
            var history = await _dbContext.Histories.FirstOrDefaultAsync(historyId => historyId.Id == id);
            if(history != null)
            {
                _dbContext.Histories.Remove(_mapper.Map<HistoryDb>(history));
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get history by user id.
        /// </summary>
        public async Task<IHistory> GetByUserIdAsync(long userId)
        {
            var historyDb = await _dbContext.Histories.AsNoTracking()
                .FirstOrDefaultAsync(h => h.UserId == userId);

            if (historyDb == null)
                return null;
            var history = _mapper.Map<History>(historyDb);
            return history;
        }

        /// <summary>
        /// Create history if not exists.
        /// </summary>
        public async Task<IHistory> CreateIfNotExistsAsync(long userId)
        {
            var existingHistory = await GetByUserIdAsync(userId);
            if (existingHistory != null)
            {
                return existingHistory;
            }

            var newHistory = new HistoryDb
            {
                UserId = userId
            };

            await _dbContext.Histories.AddAsync(newHistory);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<History>(newHistory);
        }

        /// <summary>
        /// Update history.
        /// </summary>
        public async Task UpdateAsync(IHistory history)
        {
            var existingHistory = await _dbContext.Histories.Include(t => t.User).FirstOrDefaultAsync(id => id.Id == history.Id);
            if (existingHistory != null)
            {
                 _dbContext.Histories.Update(existingHistory);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all topics from history.
        /// </summary>
        public async Task<List<ITopic>> GetAllTopicsAsync(long historyId)
        {
            var topics = await _dbContext.Topics.AsNoTracking()
                .Where(t => t.HistoryId == historyId)
                .Select(t => new Topic
                {
                    Id = t.Id,
                    Name = t.Name,
                    Messages = t.Messages.Select(m => new Message
                    {
                        Id = m.Id,
                        MessageText = m.MessageText,
                        MessageTime = m.MessageTime,
                        IsUser = m.IsUser
                    }).ToList<IMessage>()
                }).ToListAsync();

            return _mapper.Map<List<ITopic>>(topics);
        }
    }
}
