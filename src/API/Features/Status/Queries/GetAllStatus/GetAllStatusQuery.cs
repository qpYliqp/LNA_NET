using Domain.Models;
using MediatR;

namespace API.Features.Status.Queries.GetAllStatus;

public record GetAllStatusQuery() : IRequest<IReadOnlyList<Domain.Models.Status>>;
