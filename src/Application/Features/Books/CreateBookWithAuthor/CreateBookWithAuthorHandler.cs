using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Application.Features.Books.GetAllBookWithAuthor;
using Data;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.CreateBookWithAuthor;

public class CreateBookWithAuthorHandler(AppDbContext dbContext) 
    : IRequestHandler<CreateBookWithAuthorQuery, BookWithAuthorDto> // <-- Gère maintenant la Command
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<BookWithAuthorDto> Handle(CreateBookWithAuthorQuery query, CancellationToken cancellationToken)
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

        return new BookWithAuthorDto(book.Id, book.Title, book.Authors.Select(a => new AuthorDto(a.Id,a.Name)).ToList());
    }
}