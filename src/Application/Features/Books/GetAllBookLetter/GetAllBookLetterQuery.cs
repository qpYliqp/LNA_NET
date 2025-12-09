using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.GetAllBookLetter;

public record GetAllBookLetterByLetterQuery()
       : IRequest<IDictionary<string, List<BookPreviewDto>>>;