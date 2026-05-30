using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

using System.Runtime.CompilerServices;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {

        builder.HasKey(b => b.Id);

        builder.Property(b => b.GlobalStatusId)
           .HasDefaultValue(1);

        builder
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books);
    }
}