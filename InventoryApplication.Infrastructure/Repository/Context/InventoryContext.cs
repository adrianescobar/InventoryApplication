using InventoryApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApplication.Infrastructure.Repository.Context
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryContext).Assembly);

            // Seed data

            modelBuilder.Entity<EquipmentType>()
                .HasData(
                new EquipmentType { Id = 1, Description = "Laptop" },
                new EquipmentType { Id = 2, Description = "Desktop"},
                new EquipmentType { Id = 3, Description = "Printer" },
                new EquipmentType { Id = 4, Description = "Monitor" }
                );
        }
    }
}
