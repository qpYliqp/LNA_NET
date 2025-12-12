using Application.DTOs.BookDTO;
using Application.IServices;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.GetAllBookLetter;

public class GetAllBookLetterByLetterHandler(AppDbContext dbContext, IBookService bookService) : IRequestHandler<GetAllBookLetterByLetterQuery, IDictionary<string, List<BookPreviewDto>>>
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IBookService _bookService = bookService;
    public async Task<IDictionary<string, List<BookPreviewDto>>> Handle(GetAllBookLetterByLetterQuery request, CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books
            .OrderBy(b => b.Title)
            .Select(b => new {
                b.Id,
                b.Title,
                b.CoverFileName
            })
            .ToListAsync(cancellationToken);
        
        var bookPreviewTasks = books.Select(async book => new BookPreviewDto(
            book.Id,
            book.Title,
            await _bookService.GetBookCoverAsync(book.CoverFileName)
        ));
        
        var completedTasks = await Task.WhenAll(bookPreviewTasks);

        return completedTasks.Where(b =>
                !string.IsNullOrWhiteSpace(b.Title))
                    .GroupBy(b => char.ToUpper(b.Title[0]).ToString()).ToDictionary(b => b.Key, b => b.ToList());
    }
}