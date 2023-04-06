using AutoMapper;
using Database;
using Database.Entities;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddAsync(IHistory history)
        {
            await _dbContext.Histories.AddAsync(_mapper.Map<HistoryDb>(history));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var history = await _dbContext.Histories.FirstOrDefaultAsync(historyId => historyId.Id == id);
            if(history != null)
            {
                _dbContext.Histories.Remove(_mapper.Map<HistoryDb>(history));
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IHistory[]> GetByUserIdAsync(long userId)
        {
            var histories = await _dbContext.Histories
                .Include(h => h.User)
                .Include(h => h.Topics)
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.MsgTime)
                .Take(10)
                .ToArrayAsync();

            return histories.Select(h => _mapper.Map<IHistory>(h)).ToArray();
        }
    }
}
