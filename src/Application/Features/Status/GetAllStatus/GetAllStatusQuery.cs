using Application.DTOs.ProductionStepDTO;
using Application.DTOs.StatusDTO;
using MediatR;

namespace Application.Features.Status.GetAllStatus;

public record GetAllStatusQuery() : IRequest<IReadOnlyList<StatusDto>>;