using System.Linq.Expressions;
using Application.DTOs.AuthorDTO;
using Domain.Entities;

namespace Application.Mappers;

public static class AuthorMappingExtension
{
    private static Expression<Func<Author, AuthorDto>> ProjectToDto =>
        author => new AuthorDto(
            Id : author.Id,
            Name : author.Name
        );

    public static AuthorDto ToDto(this Author author)
    {
        return author == null ? null : ProjectToDto.Compile()(author);
    }
}