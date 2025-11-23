using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.GetAllBookWithAuthor;

public class GetAllBookWithAuthorHandler(AppDbContext dbContext) : IRequestHandler<GetAllBookWithAuthorQuery, IReadOnlyList<BookWithAuthorDto>>
{
    
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<BookWithAuthorDto>> Handle(GetAllBookWithAuthorQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Books
            .Include(b => b.Authors) 
            .Select(book => new BookWithAuthorDto(
                book.Id,
                book.Title,
                book.Authors.Select(author => new AuthorDto(
                    author.Id,
                    author.Name
                )).ToList() 
            ))
            .ToListAsync(cancellationToken);
    }
    
}