using Domain.Enumerations;
using Domain.Interfaces.Services;
using Domain.Models;
using Infrastructure;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Books.Commands.UploadBookCover;

public class UploadBookCoverHandler(AppDbContext _dbContext, IMinioService _minioService)
    : IRequestHandler<UploadBookCoverCommand, Book>
{
    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    

    public async Task<Book> Handle(UploadBookCoverCommand command, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.BookSteps)
            .FirstOrDefaultAsync(b => b.Id == command.BookId, cancellationToken)
            ?? throw new BookDomainException($"Le livre {command.BookId} est introuvable.");

        var extension = Path.GetExtension(command.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
        {
            throw new BookDomainException(
                $"Format d'image non supporté ({extension}). Formats acceptés : {string.Join(", ", AllowedExtensions)}.");
        }

        // Clé unique pour éviter les collisions / problèmes de cache.
        var objectKey = $"{Guid.NewGuid():N}{extension}";

        await _minioService.UploadFileAsync(
            command.Content,
            objectKey,
            command.ContentType,
            BucketNames.Cover);

        book.CoverFileName = objectKey;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return book.ToModel();
    }
}
