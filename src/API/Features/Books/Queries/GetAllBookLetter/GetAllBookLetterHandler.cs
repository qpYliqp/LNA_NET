using Domain.Models;
using Domain.Interfaces.Services;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Books.Queries.GetAllBookLetter;

public class GetAllBookLetterByLetterHandler(AppDbContext dbContext, IBookService bookService)
    : IRequestHandler<GetAllBookLetterByLetterQuery, IDictionary<string, List<BookPreview>>>
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IBookService _bookService = bookService;

    public async Task<IDictionary<string, List<BookPreview>>> Handle(GetAllBookLetterByLetterQuery request,
        CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books
            .AsNoTracking()
            .OrderBy(b => b.Title)
            .Select(b => new { b.Id, b.Title, b.CoverFileName })
            .ToListAsync(cancellationToken);

        var previews = await Task.WhenAll(books.Select(async book => new BookPreview(
            book.Id,
            book.Title,
            await _bookService.GetBookCoverAsync(book.CoverFileName))));

        return previews
            .Where(b => !string.IsNullOrWhiteSpace(b.Title))
            .GroupBy(b => char.ToUpper(b.Title[0]).ToString())
            .ToDictionary(g => g.Key, g => g.ToList());
    }
}
