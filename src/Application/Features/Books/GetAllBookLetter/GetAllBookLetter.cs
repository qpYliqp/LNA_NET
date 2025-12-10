using Application.DTOs.BookDTO;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.GetAllBookLetter;

public class GetAllBookLetterByLetterHandler(AppDbContext dbContext) : IRequestHandler<GetAllBookLetterByLetterQuery, IDictionary<string, List<BookPreviewDto>>>
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task<IDictionary<string, List<BookPreviewDto>>> Handle(GetAllBookLetterByLetterQuery request, CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books
            .OrderBy(b => b.Title)
            .Select(b => new BookPreviewDto(
                b.Id,
                b.Title,
                ""
            ))
            .ToListAsync(cancellationToken);

        return books.Where(b =>
                !string.IsNullOrWhiteSpace(b.Title))
                    .GroupBy(b => char.ToUpper(b.Title[0]).ToString()).ToDictionary(b => b.Key, b => b.ToList());
    }
}