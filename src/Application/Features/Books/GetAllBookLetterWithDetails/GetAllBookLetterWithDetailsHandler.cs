using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.Features.Books.GetAllBookWithAuthor;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Application.Features.Books.GetAllBookLetterByLetter;

public class GetAllBookLetterByLetterHandler(AppDbContext dbContext) : IRequestHandler<GetAllBookLetterByLetterQuery, IDictionary<string, List<BookLetterWithDetailsDto>>>
{

    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IDictionary<string, List<BookLetterWithDetailsDto>>> Handle(GetAllBookLetterByLetterQuery request, CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books
            .OrderBy(b => b.Title)
            .Select(b => new BookLetterWithDetailsDto(
                b.Id,
                b.Title
            ))
            .ToListAsync(cancellationToken);

        return books.Where(b =>
                !string.IsNullOrWhiteSpace(b.Title))
                    .GroupBy(b => char.ToUpper(b.Title[0]).ToString()).ToDictionary(b => b.Key, b => b.ToList());
    }
}