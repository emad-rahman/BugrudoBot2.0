using BugrudoBot.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BugrudoBot.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Bug> Bugs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(ConnectionStringHelper.GetConnectionString());
    }
}