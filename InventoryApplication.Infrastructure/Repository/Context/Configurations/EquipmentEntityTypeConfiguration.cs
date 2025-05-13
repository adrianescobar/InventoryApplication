using InventoryApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryApplication.Infrastructure.Repository.Context.Configurations
{
    internal class EquipmentEntityTypeConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.ToTable("Equipment");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().UseHiLo();
            builder.Property(e => e.Brand).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Model).IsRequired().HasMaxLength(100);
            builder.HasOne(e => e.EquipmentType)
                .WithMany(EquipmentType => EquipmentType.Equipments)
                .HasForeignKey(e => e.EquipmentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.PurchaseDate).IsRequired();
            builder.Property(e => e.SerialNumber).HasMaxLength(100);
        }
    }
}
