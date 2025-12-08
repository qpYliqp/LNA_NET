using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.GetAllBookWithAuthor;

public record GetAllBookLetterWithDetailsQuery()
       : IRequest<IDictionary<string, List<BookLetterWithDetailsDto>>>;