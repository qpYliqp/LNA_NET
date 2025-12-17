using System.Linq.Expressions;
using Application.DTOs.AuthorDTO;
using Domain.Entities;

public record BookDto(
    int Id,
    string Title,
    string Isbn,
    string? Nuart,
    int? Pages,
    float? Price,
    string? Note,
    string? Summary,
    string? Hook,
    string? Marketing,
    string? CoverFileName,
    DateTime? ReleaseDate,
    ICollection<AuthorDto> Authors)
{
    
}