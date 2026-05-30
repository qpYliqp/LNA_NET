using Infrastructure;
using Infrastructure.Mappers;
using Infrastructure.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Myth.Specifications;
using DomainBook = Domain.Models.Book;
using EntityBook = Infrastructure.Entities.Book;

namespace API.Features.Books.Queries.GetAllBook;

public class GetAllBookHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllBookQuery, IReadOnlyList<DomainBook>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<DomainBook>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
    {
        var spec = SpecBuilder<EntityBook>.Create().StartsWith(request.Prefix);

        var books = await _dbContext.Books
            .AsNoTracking()
            .AsSplitQuery()
            .Include(b => b.Authors)
            .Include(b => b.BookSteps)
            .Where(spec.Predicate)
            .ToListAsync(cancellationToken);

        return books.Select(b => b.ToModel()).ToList();
    }
}
