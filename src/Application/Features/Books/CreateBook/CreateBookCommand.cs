using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.CreateBook;


public record CreateBookCommand(
    CreateBookRequestDto BookDetails
) : IRequest<BookDto>;
