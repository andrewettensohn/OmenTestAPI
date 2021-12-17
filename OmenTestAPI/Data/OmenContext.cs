using Microsoft.EntityFrameworkCore;
using OmenModels;

namespace OmenTestAPI.Data
{
    public class OmenContext : DbContext
    {
        public OmenContext(DbContextOptions<OmenContext> options) : base(options) { }

        public DbSet<Starship> Starships { get; set; }
        public DbSet<StarshipHull> StarshipHulls { get; set; }
        public DbSet<ShipModule> ShipModules { get; set; }
        public DbSet<StarshipClass> StarshipClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShipModule>().HasData(new ShipModule
            {
                Name = "Empty Module Slot",
                Category = ModuleCategory.EmptySlot,
                DamageType = DamageType.None,
                Description = "A section of the ship's hull that is meant to have a module installed.",
                Id = Guid.NewGuid(),
            });
        }
    }
}
