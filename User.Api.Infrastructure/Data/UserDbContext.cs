using Microsoft.EntityFrameworkCore;
using User.Api.Domain.Entities;

namespace User.Api.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(p => new { p.UserName })
                .IsUnique(true);
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
