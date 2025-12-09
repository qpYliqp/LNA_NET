using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.GetAllBook;

 public record GetAllBookQuery() 
        : IRequest<IReadOnlyList<BookDto>>;