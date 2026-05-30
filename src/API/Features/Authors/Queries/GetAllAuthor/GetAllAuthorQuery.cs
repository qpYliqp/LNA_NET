using Domain.Models;
using MediatR;

namespace API.Features.Authors.Queries.GetAllAuthor;

public record GetAllAuthorQuery() : IRequest<IReadOnlyList<Author>>;
