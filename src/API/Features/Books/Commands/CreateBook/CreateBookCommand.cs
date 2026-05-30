using Domain.Models;
using MediatR;

namespace API.Features.Books.Commands.CreateBook;

public record CreateBookCommand(CreateBookRequest Request) : IRequest<Book>;
