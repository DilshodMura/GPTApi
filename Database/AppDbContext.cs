using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext:DbContext
    {
        public DbSet<HistoryDb> Histories { get; set; }

        public DbSet<UserDb> Users { get;set; }

        public DbSet<MessageDb> Messages { get; set; }

        public DbSet<TopicDb> Topics { get; set; }
        public DbSet<RefreshTokenDb> RefreshTokens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
                    : base(options)
        {
        }
    }
}
