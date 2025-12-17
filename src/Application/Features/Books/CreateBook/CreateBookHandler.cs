
using Application.IServices;
using Application.Mappers;
using Data;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Books.CreateBook;

public class CreateBookHandler(AppDbContext dbContext, IBookService bookService) 
    : IRequestHandler<CreateBookCommand, BookDto> // <-- Gère maintenant la Command
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly IBookService _bookService = bookService;

    public async Task<BookDto> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var bookCommand = command.BookDetails; 
        
        var authors = await _dbContext.Authors
            .Where(a => bookCommand.AuthorsId.Contains(a.Id))
            .ToListAsync(cancellationToken);
        
        var book = bookCommand.ToEntity();
        book.Authors = authors;

        if (bookCommand.BookSteps.Count == await this._dbContext.ProductionSteps.CountAsync(cancellationToken))
        {
            foreach (var step in bookCommand.BookSteps)
            {
                if (step.EndDate > book.ReleaseDate)
                {
                    throw new BadHttpRequestException(
                        "Une étape de production ne peut avoir une date supérieure à la date de sortie du livre");
                }
                book.AddBookStep(step.ToEntity());
            }
        }
        else
        {
            throw new BadHttpRequestException("Il n'y a pas le bon nombre d'étapes de production.");
        }
        book.updateGlobalStatus();
        _dbContext.Books.Add(book);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return book.ToDto();
    }
}