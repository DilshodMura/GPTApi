using AutoMapper;
using Database;
using Database.Entities;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositoriy
{
    public sealed class UserRepository : IUserRepository
    {
        public readonly AppDbContext _dbContext;
        public readonly IMapper _mapper;

        public UserRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(IUser user)
        {
            await _dbContext.Users.AddAsync(_mapper.Map<UserDb>(user));
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IUser[]> GetAllsync()
        {
            var users = await _dbContext.Users.ToArrayAsync();
            return _mapper.Map<IUser[]>(users);
        }

        public async Task UpdateAsync(long id, IUser user)
        {
            var userDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userDb != null)
            {
                _mapper.Map(user, userDb);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<IUser> GetByIdAsync(long id)
        {
            var userDb = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
            return _mapper.Map<IUser>(userDb);
        }

    }
}
