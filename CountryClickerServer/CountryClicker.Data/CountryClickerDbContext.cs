using CountryClicker.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace CountryClicker.Data
{
    public class CountryClickerDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CustomGroup> CustomGroups { get; set; }
        public DbSet<GroupSprint> GroupSprints { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerSprint> PlayerSprints { get; set; }
        public DbSet<PlayerSubscription> PlayerSubscriptions { get; set; }
        public DbSet<Sprint> Sprints { get; set; }

        public CountryClickerDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupSprint>().HasKey(entity => new { entity.GroupId, entity.SprintId });
            modelBuilder.Entity<PlayerSprint>().HasKey(entity => new { entity.PlayerId, entity.SprintId });
            modelBuilder.Entity<PlayerSubscription>().HasKey(entity => new { entity.PlayerId, entity.GroupId });
        }
    }
}
