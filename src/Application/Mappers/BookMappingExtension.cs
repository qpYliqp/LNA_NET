using System.Linq.Expressions;
using Application.DTOs.AuthorDTO;
using Application.DTOs.BookDTO;
using Domain.Entities;

namespace Application.Mappers;

public static class BookMappingExtension
{
    // L'expression de projection (pour IQueryable / SQL)
    private static Expression<Func<Book, BookDto>> ProjectToDto =>
        book => new BookDto(
            Id : book.Id,
            Title : book.Title,
            Isbn: book.Isbn,
            Nuart: book.Nuart,
            Pages: book.Pages,
            Price: book.Price,
            Note: book.Note,
            Summary: book.Summary,
            Hook: book.Hook,
            Marketing: book.Marketing,
            CoverFileName: book.CoverFileName,
            ReleaseDate: book.ReleaseDate,
            Authors: book.Authors.Select(author => new AuthorDto(
                author.Id,
                author.Name
            )).ToList()
        );

    public static BookDto ToDto(this Book book)
    {
        return book == null ? null : ProjectToDto.Compile()(book);
    }
}