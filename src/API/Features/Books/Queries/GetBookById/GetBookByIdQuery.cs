using Domain.Models;
using MediatR;

namespace API.Features.Books.Queries.GetBookById;

public record GetBookByIdQuery(int BookId) : IRequest<Book>;
