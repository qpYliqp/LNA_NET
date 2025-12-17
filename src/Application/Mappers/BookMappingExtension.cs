using System.Linq.Expressions;
using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Domain.Entities;

namespace Application.Mappers;

public static class BookMappingExtension
{
    // L'expression de projection (pour IQueryable / SQL)
    public static Expression<Func<Book, BookDto>> ProjectToDto =>
        book => new BookDto(
            book.Id,
            book.Title,
            book.Isbn,
            book.Nuart,
            book.Pages,
            book.Price,
            book.Note,
            book.Summary,
            book.Hook,
            book.Marketing,
            book.CoverFileName,
            book.ReleaseDate,
            book.Authors.Select(author => new AuthorDto(
                author.Id,
                author.Name
            )).ToList()
        );

    public static BookDto ToDto(this Book book)
    {
        return book == null ? null : ProjectToDto.Compile()(book);
    }
}