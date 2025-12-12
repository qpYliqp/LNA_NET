using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.GetAllBook;

 public record GetAllBookQuery(string? prefix) 
        : IRequest<IReadOnlyList<BookDto>>;