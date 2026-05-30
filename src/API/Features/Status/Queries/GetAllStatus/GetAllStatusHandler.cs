using Infrastructure;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StatusModel = Domain.Models.Status;

namespace API.Features.Status.Queries.GetAllStatus;

public class GetAllStatusHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllStatusQuery, IReadOnlyList<StatusModel>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<StatusModel>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
    {
        var statuses = await _dbContext.Status
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return statuses.Select(s => s.ToModel()).ToList();
    }
}
