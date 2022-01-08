using GameTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Server.Contexts
{
    public class GameContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        // virtual member to allow unit tests to override
        public virtual DbSet<Game> Games => Set<Game>();

        // empty constructor to allow unit tests to create an instance
        public GameContext () { }

        public GameContext(IConfiguration configuration)
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
