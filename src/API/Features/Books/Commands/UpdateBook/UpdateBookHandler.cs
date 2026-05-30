using Domain.Models;
using Infrastructure;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Books.Commands.UpdateBook;

public class UpdateBookHandler(AppDbContext dbContext)
    : IRequestHandler<UpdateBookCommand, Book>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Book> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var entity = await _dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.BookSteps)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken)
            ?? throw new BookDomainException($"Le livre {request.Id} est introuvable.");

        var authors = await _dbContext.Authors
            .Where(a => request.AuthorsId.Contains(a.Id))
            .ToListAsync(cancellationToken);

        var expectedStepCount = await _dbContext.ProductionSteps.CountAsync(cancellationToken);
        if (request.BookSteps.Count != expectedStepCount)
        {
            throw new BookDomainException(
                $"Il n'y a pas le bon nombre d'étapes de production. Reçu: {request.BookSteps.Count}, Attendu: {expectedStepCount}");
        }

        // On rejoue les règles métier sur le modèle de domaine.
        var model = request.ToModel();
        foreach (var step in request.BookSteps)
        {
            model.AddBookStep(step.ToModel());
        }
        model.UpdateGlobalStatus();

        // On reporte l'état validé sur l'entité suivie par EF.
        entity.Title = model.Title;
        entity.Isbn = model.Isbn;
        entity.Nuart = model.Nuart;
        entity.Pages = model.Pages;
        entity.Price = model.Price;
        entity.Note = model.Note;
        entity.Summary = model.Summary;
        entity.Hook = model.Hook;
        entity.Marketing = model.Marketing;
        entity.CoverFileName = model.CoverFileName;
        entity.ReleaseDate = model.ReleaseDate;
        entity.GlobalStatusId = model.GlobalStatusId;
        entity.Authors = authors;
        entity.BookSteps = model.BookSteps.Select(s => s.ToEntity()).ToList();

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.ToModel();
    }
}
