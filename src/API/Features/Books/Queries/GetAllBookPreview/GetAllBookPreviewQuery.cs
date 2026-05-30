using Domain.Models;
using MediatR;

namespace API.Features.Books.Queries.GetAllBookPreview;

public record GetAllBookPreviewQuery() : IRequest<IReadOnlyList<BookPreview>>;
