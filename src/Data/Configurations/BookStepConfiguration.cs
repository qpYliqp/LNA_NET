using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class BookStepConfiguration  : IEntityTypeConfiguration<BookStep>
{
    public void Configure(EntityTypeBuilder<BookStep> builder)
    {
        builder.HasKey(bs => bs.Id);
        
        builder.HasOne(bs => bs.Book)
            .WithMany(b => b.BookSteps)
            .HasForeignKey(bs => bs.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bs => bs.ProductionStep)
            .WithMany()
            .HasForeignKey(bs => bs.ProductionStepId)
            .OnDelete(DeleteBehavior.Restrict); //Spécifie qu'on ne supprime pas le statut quand on supprime un bookstep
        
        builder.HasOne(bs => bs.Status)
            .WithMany()
            .HasForeignKey(bs => bs.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Empêche d'avoir deux lignes pour le même Livre + même Étape
        builder.HasIndex(bs => new { bs.BookId, bs.ProductionStepId }).IsUnique();
    }
}