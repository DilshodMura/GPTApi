using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext:DbContext
    {
        public DbSet<History> History { get; set; }

        public DbSet<User> Users { get;set; }

        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
                    : base(options)
        {
        }
    }
}
