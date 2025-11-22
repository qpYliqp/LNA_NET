namespace Application.DTOs.BookDTO;
using Domain.Entities;

public record CreateBookRequestDto(
    string Title,
    ICollection<int> AuthorsId);