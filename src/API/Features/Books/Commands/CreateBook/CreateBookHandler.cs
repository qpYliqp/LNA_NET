using Domain.Models;
using Infrastructure;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Books.Commands.CreateBook;

public class CreateBookHandler(AppDbContext dbContext)
    : IRequestHandler<CreateBookCommand, Book>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Book> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var authors = await _dbContext.Authors
            .Where(a => request.AuthorsId.Contains(a.Id))
            .ToListAsync(cancellationToken);

        var expectedStepCount = await _dbContext.ProductionSteps.CountAsync(cancellationToken);
        if (request.BookSteps.Count != expectedStepCount)
        {
            throw new BookDomainException(
                $"Il n'y a pas le bon nombre d'étapes de production. Reçu: {request.BookSteps.Count}, Attendu: {expectedStepCount}");
        }

        var model = request.ToModel();
        foreach (var step in request.BookSteps)
        {
            model.AddBookStep(step.ToModel());
        }
        model.UpdateGlobalStatus();

        var entity = model.ToEntity();
        entity.Authors = authors;

        _dbContext.Books.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.ToModel();
    }
}
