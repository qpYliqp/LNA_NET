using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.Enumerations;
using Application.Features.Books.GetAllBook;
using Application.IServices;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.GetAllBookPreview;

public class GetAllBookPreviewHandler(AppDbContext dbContext, IBookService bookService) : IRequestHandler<GetAllBookPreviewQuery, IReadOnlyList<BookPreviewDto>>
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IBookService _bookService = bookService;
    public async Task<IReadOnlyList<BookPreviewDto>> Handle(GetAllBookPreviewQuery request,
        CancellationToken cancellationToken)
    {
        var books = await _dbContext.Books
            .Include(b => b.Authors) 
            .Select(book => new 
            {
                book.Id,
                book.Title,
                book.CoverFileName
            })
            .ToListAsync(cancellationToken);
        
        var bookPreviewTasks = books.Select(async book => new BookPreviewDto(
            book.Id,
            book.Title,
            await _bookService.GetBookCoverAsync(book.CoverFileName)
        ));
        return await Task.WhenAll(bookPreviewTasks);
    }
    
}