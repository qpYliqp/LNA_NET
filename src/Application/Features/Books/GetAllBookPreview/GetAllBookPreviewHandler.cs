using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.Features.Books.GetAllBook;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.GetAllBookPreview;

public class GetAllBookPreviewHandler(AppDbContext dbContext) : IRequestHandler<GetAllBookPreviewQuery, IReadOnlyList<BookPreviewDto>>
{
    
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<BookPreviewDto>> Handle(GetAllBookPreviewQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Books
            .Include(b => b.Authors) 
            .Select(book => new BookPreviewDto(
                book.Id,
                book.Title 
            )).ToListAsync(cancellationToken);
    }
    
}