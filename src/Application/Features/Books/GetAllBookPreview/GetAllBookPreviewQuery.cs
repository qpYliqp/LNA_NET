using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Books.GetAllBookPreview;

 public record GetAllBookPreviewQuery() 
        : IRequest<IReadOnlyList<BookPreviewDto>>;