using InventoryApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace InventoryApplication.Infrastructure.Repository.Context.Configurations
{
    public class MaintenanceTaskEntityTypeConfiguration : IEntityTypeConfiguration<MaintenanceTask>
    {
        public void Configure(EntityTypeBuilder<MaintenanceTask> builder)
        {
            builder.ToTable("MaintenanceTask");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().UseHiLo();
            builder.Property(e => e.Description).HasMaxLength(100).IsRequired();
            builder.HasMany(e => e.Equipments)
                .WithMany(e => e.MaintenanceTasks)
                .UsingEntity<Dictionary<string, object>>(
                "EquipmentMaintenanceTask",
                                    j => j.HasOne<Equipment>()
                                    .WithMany()
                                    .HasForeignKey("EquipmentId")
                                    .OnDelete(DeleteBehavior.Cascade),
                                            j => j.HasOne<MaintenanceTask>()
                                                .WithMany()
                                                .HasForeignKey("MaintenanceTaskId")
                                                    .OnDelete(DeleteBehavior.Cascade),
                                                        j =>
                                                        {
                                                            j.HasKey("EquipmentId", "MaintenanceTaskId");
                                                            j.ToTable("EquipmentMaintenanceTask");
                                                        });
        }
    }
}
