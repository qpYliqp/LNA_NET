using Domain.Models;
using MediatR;

namespace API.Features.Books.Commands.UpdateBook;

public record UpdateBookCommand(UpdateBookRequest Request) : IRequest<Book>;
