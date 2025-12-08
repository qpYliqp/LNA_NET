using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.GetAllBookWithAuthor;

public record GetAllBookLetterByLetterQuery()
       : IRequest<IDictionary<string, List<BookLetterWithDetailsDto>>>;