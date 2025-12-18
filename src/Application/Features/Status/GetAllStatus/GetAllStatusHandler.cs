using Application.DTOs.ProductionStepDTO;
using Application.DTOs.StatusDTO;
using Application.Features.ProductionStep;
using Application.Mappers;
using Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Status.GetAllStatus;

public class GetAllStatusHandler(AppDbContext dbContext) : IRequestHandler<GetAllStatusQuery, IReadOnlyList<StatusDto>>
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyList<StatusDto>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Status
            .AsNoTracking()
            .Select(s => s.ToDto())
            .ToListAsync(cancellationToken);
    }
}