using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.Enumerations;
using Application.Features.Books.GetAllBook;
using Application.IServices;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.GetAllBookPreview;

public class GetAllBookPreviewHandler(AppDbContext dbContext, IMinioService minioService) : IRequestHandler<GetAllBookPreviewQuery, IReadOnlyList<BookPreviewDto>>
{
    
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IMinioService _minioService = minioService;

    public async Task<IReadOnlyList<BookPreviewDto>> Handle(GetAllBookPreviewQuery request,
        CancellationToken cancellationToken)
    {
        var booksWithFileName = await _dbContext.Books
            .Include(b => b.Authors) 
            .Select(book => new 
            {
                book.Id,
                book.Title
            })
            .ToListAsync(cancellationToken);
        
        var bookPreviewTasks = booksWithFileName.Select(async book => 
        {
            string? coverUrl = await _minioService.getFileUrlByNameAsync("bk1.jpg", BucketNames.Cover);
            return new BookPreviewDto(
                book.Id,
                book.Title,
                coverUrl
            );
        });
        
        return await Task.WhenAll(bookPreviewTasks);
    }
    
}