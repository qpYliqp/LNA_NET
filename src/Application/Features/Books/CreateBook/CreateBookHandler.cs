using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Data;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.CreateBook;

public class CreateBookHandler(AppDbContext dbContext) 
    : IRequestHandler<CreateBookQuery, BookDto> // <-- Gère maintenant la Command
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<BookDto> Handle(CreateBookQuery query, CancellationToken cancellationToken)
    {
        var request = query.BookDetails; 
        
        var authors = await _dbContext.Set<Author>()
            .Where(a => request.AuthorsId.Contains(a.Id))
            .ToListAsync(cancellationToken);
        
        var book = new Book
        {
            Title = request.Title,
            Authors = authors,
        };

        _dbContext.Set<Book>().Add(book);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new BookDto(book.Id, book.Title, book.Authors.Select(a => new AuthorDto(a.Id,a.Name)).ToList());
    }
}