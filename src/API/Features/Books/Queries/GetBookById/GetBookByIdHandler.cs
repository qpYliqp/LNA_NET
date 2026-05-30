using Domain.Models;
using Infrastructure;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Books.Queries.GetBookById;

public class GetBookByIdHandler(AppDbContext dbContext)
    : IRequestHandler<GetBookByIdQuery, Book>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Books
            .AsNoTracking()
            .AsSplitQuery()
            .Include(b => b.Authors)
            .Include(b => b.BookSteps)
            .FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken)
            ?? throw new BookDomainException($"Le livre {request.BookId} est introuvable.");

        return entity.ToModel();
    }
}
