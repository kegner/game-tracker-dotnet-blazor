using GameTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Server.Contexts
{
    public class UserContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        public virtual DbSet<User> Users => Set<User>();

        public UserContext() { }

        public UserContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = _configuration.GetConnectionString("gameTrackerDb");
            optionsBuilder.UseNpgsql(conn);
        }
    }
}
