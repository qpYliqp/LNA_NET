using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using MediatR;

namespace Application.Features.Authors.GetAllAuthor;

 public record GetAllAuthorQuery() 
        : IRequest<IReadOnlyList<AuthorDto>>;