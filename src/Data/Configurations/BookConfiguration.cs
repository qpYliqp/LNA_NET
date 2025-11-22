using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;
using Domain.Entities;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {

        builder.HasKey(b => b.Id);

        // 3. Exemple de données de départ (Seeding)
        builder.HasData(
            new Book { Id = 1, Title = "One Piece" }
        );
    }
}