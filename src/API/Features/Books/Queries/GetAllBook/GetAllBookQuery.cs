using Domain.Models;
using MediatR;

namespace API.Features.Books.Queries.GetAllBook;

public record GetAllBookQuery(string? Prefix) : IRequest<IReadOnlyList<Book>>;
