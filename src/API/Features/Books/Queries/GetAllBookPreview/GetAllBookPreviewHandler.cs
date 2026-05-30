using Domain.Models;
using Domain.Interfaces.Services;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Books.Queries.GetAllBookPreview;

public class GetAllBookPreviewHandler(AppDbContext dbContext, IBookService bookService)
    : IRequestHandler<GetAllBookPreviewQuery, IReadOnlyList<BookPreview>>
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IBookService _bookService = bookService;

    public async Task<IReadOnlyList<BookPreview>> Handle(GetAllBookPreviewQuery request,
        CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books
            .AsNoTracking()
            .Select(book => new { book.Id, book.Title, book.CoverFileName })
            .ToListAsync(cancellationToken);

        var previews = await Task.WhenAll(books.Select(async book => new BookPreview(
            book.Id,
            book.Title,
            await _bookService.GetBookCoverAsync(book.CoverFileName))));

        return previews;
    }
}
