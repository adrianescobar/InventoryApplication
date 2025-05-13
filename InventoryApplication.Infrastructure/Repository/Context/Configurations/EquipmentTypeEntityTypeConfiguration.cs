using InventoryApplication.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryApplication.Infrastructure.Repository.Context.Configurations
{
    public class EquipmentTypeEntityTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
    {
        public void Configure(EntityTypeBuilder<EquipmentType> builder)
        {
            builder.ToTable("EquipmentType");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd().UseHiLo();
            builder.Property(e => e.Description).HasMaxLength(100).IsRequired();
        }
    }
}
