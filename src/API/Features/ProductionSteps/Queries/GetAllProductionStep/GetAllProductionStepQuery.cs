using Domain.Models;
using MediatR;

namespace API.Features.ProductionSteps.Queries.GetAllProductionStep;

public record GetAllProductionStepQuery() : IRequest<IReadOnlyList<ProductionStep>>;
