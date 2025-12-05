using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.CreateBookWithAuthor;


public record CreateBookWithAuthorQuery(
    CreateBookRequestDto BookDetails
) : IRequest<BookWithAuthorDto>;
