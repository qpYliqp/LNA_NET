using Domain.Models;
using Infrastructure;
using Infrastructure.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.ProductionSteps.Queries.GetAllProductionStep;

public class GetAllProductionStepHandler(AppDbContext dbContext)
    : IRequestHandler<GetAllProductionStepQuery, IReadOnlyList<ProductionStep>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<ProductionStep>> Handle(GetAllProductionStepQuery request, CancellationToken cancellationToken)
    {
        var steps = await _dbContext.ProductionSteps
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return steps.Select(ps => ps.ToModel()).ToList();
    }
}
