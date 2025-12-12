using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.GetAllBookWithAuthor;

 public record GetAllBookWithAuthorQuery(string? prefix) 
        : IRequest<IReadOnlyList<BookDto>>;