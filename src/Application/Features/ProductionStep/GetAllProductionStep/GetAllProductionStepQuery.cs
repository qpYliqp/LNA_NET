using Application.DTOs.ProductionStepDTO;
using MediatR;

namespace Application.Features.ProductionStep.GetAllProductionStep;

public record GetAllProductionStepQuery() : IRequest<IReadOnlyList<ProductionStepDto>>;