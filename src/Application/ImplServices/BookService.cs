using Application.DTOs.BookDTO;
using Application.DTOs.BookStepDTO;
using Application.Enumerations;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.ImplServices;

public class BookService(IMinioService minioService) : IBookService
{
    private readonly IMinioService _minioService = minioService;
    public async Task<string?> GetBookCoverAsync(string? coverFileName)
    {
        string? coverUrl = null;
        if (coverFileName != null)
        {
            coverUrl = await _minioService.GetFileUrlByNameAsync(coverFileName, BucketNames.Cover);
        }
        return coverUrl;
    }

    public void ConfigureBookSteps(Book book, ICollection<CreateBookStepDto> bookSteps)
    {
        foreach (var step in bookSteps)
        {
            if (step.EndDate > book.ReleaseDate)
            {
                throw new BadHttpRequestException(
                    "Une étape de production ne peut avoir une date supérieure à la date de sortie du livre");
            }
            book.BookSteps.Add(step.ToEntity());
        }
    }

    
}