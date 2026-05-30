using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProductionStepConfiguration
    : IEntityTypeConfiguration<ProductionStep>
{
    public void Configure(EntityTypeBuilder<ProductionStep> builder)
    {
        builder.HasKey(ps => ps.Id);
    }
}