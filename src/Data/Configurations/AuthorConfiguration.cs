using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    
    public void Configure(EntityTypeBuilder<Author> builder)
    {

        builder.HasKey(a => a.Id);
        
        builder.HasData(
            new Author { Id = 1, Name = "Eiichiro Oda" }
        );
    }
    
}