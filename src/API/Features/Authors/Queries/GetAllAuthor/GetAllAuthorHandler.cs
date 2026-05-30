using Domain.Models;
using Infrastructure;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Authors.Queries.GetAllAuthor;

public class GetAllAuthorHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllAuthorQuery, IReadOnlyList<Author>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<Author>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
    {
        var authors = await _dbContext.Authors
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return authors.Select(a => a.ToModel()).ToList();
    }
}
