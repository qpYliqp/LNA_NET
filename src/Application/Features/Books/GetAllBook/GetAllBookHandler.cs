using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.GetAllBook;

public class GetAllBookHandler(AppDbContext dbContext) : IRequestHandler<GetAllBookQuery, IReadOnlyList<BookDto>>
{
    private readonly AppDbContext _dbContext = dbContext;
    public async Task<IReadOnlyList<BookDto>> Handle(GetAllBookQuery request,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Books
            .Include(b => b.Authors) 
            .Select(book => new BookDto(
                book.Id,
                book.Title,
                book.Authors.Select(author => new AuthorDto(
                    author.Id,
                    author.Name)).ToList() 
            ))
            .ToListAsync(cancellationToken);
    }
}