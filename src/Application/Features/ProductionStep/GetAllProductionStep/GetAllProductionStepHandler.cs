using Application.DTOs.ProductionStepDTO;
using Application.Mappers;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductionStep.GetAllProductionStep;

public class GetAllProductionStepHandler(AppDbContext dbContext) : IRequestHandler<GetAllProductionStepQuery, IReadOnlyList<ProductionStepDto>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<ProductionStepDto>> Handle(GetAllProductionStepQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.ProductionSteps
            .AsNoTracking()
            .Select(ps => ps.ToDto())
            .ToListAsync(cancellationToken);
    }
}