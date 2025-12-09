using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.CreateBookWithAuthor;


public record CreateBookQuery(
    CreateBookRequestDto BookDetails
) : IRequest<BookDto>;
