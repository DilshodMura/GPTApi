using AutoMapper;
using Database;
using Database.Entities;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
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

        /// <summary>
        /// Add user.
        /// </summary>
        public async Task AddAsync(IUser user)
        {
            await _dbContext.Users.AddAsync(_mapper.Map<UserDb>(user));
            await _dbContext.SaveChangesAsync();
        }
        
        /// <summary>
        /// Delete user.
        /// </summary>
        public async Task DeleteAsync(long id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        public async Task<IUser[]> GetAllsync()
        {
            var users = await _dbContext.Users.AsNoTracking().ToArrayAsync();
            return _mapper.Map<IUser[]>(users);
        }

        /// <summary>
        /// Update user.
        /// </summary>
        public async Task UpdateAsync(long id, IUser user)
        {
            var userDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userDb != null)
            {
                _mapper.Map(user, userDb);
                await _dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get user by id. 
        /// </summary>
        public async Task<IUser> GetByIdAsync(long id)
        {
            var userDb = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
            return _mapper.Map<IUser>(userDb);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        public async Task<IUser> GetUserByEmail(string email)
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<UserDb, IUser>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Email, src => src.Email);

            var user = await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Email == email)
                .ProjectToType<IUser>(config)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
