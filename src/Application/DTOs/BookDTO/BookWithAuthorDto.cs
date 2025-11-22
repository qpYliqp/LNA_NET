using Application.DTOs.AuthorDTO;

namespace Application.DTOs.BookDTO;
using Domain.Entities;

public record BookWithAuthorDto(
    int Id,
    string Title,
    ICollection<AuthorDto> Authors);