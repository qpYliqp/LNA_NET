using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;
using Domain.Entities;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {

        builder.HasKey(b => b.Id);
        
        builder.HasData(
            new Book { Id = 1, Title = "One Piece" }
        );
    }
}