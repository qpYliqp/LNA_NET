using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.GetAllBookWithAuthor;

 public record GetAllBookWithAuthorQuery() 
        : IRequest<IReadOnlyList<BookWithAuthorDto>>;