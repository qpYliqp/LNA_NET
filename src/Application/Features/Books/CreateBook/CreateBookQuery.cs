using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.CreateBook;


public record CreateBookQuery(
    CreateBookRequestDto BookDetails
) : IRequest<BookDto>;
