using BackendChallenge.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendChallenge.Infrastructure.Context
{
    public class BcContext : DbContext
    {
        public BcContext(DbContextOptions<BcContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteWord>().HasKey(fw => new { fw.UserId, fw.Word });
            modelBuilder.Entity<UserHistory>().HasKey(uh => new { uh.UserId, uh.Word, uh.Added });
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Word>? Words { get; set; }
        public DbSet<FavoriteWord>? FavoriteWords { get; set; }
        public DbSet<UserHistory>? UserHistories { get; set; }
    }
}
