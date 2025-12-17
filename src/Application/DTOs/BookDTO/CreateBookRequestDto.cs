using Application.DTOs.AuthorDTO;
using Application.DTOs.BookStepDTO;

namespace Application.DTOs.BookDTO;
using Domain.Entities;

public record CreateBookRequestDto(
    string Title,
    string Isbn,
    string Nuart,
    int? Pages,
    int? Price,
    string Note,
    string Summary,
    string Hook,
    string Marketing,
    string CoverFileName,
    DateTime? ReleaseDate,
    ICollection<int> AuthorsId,
    ICollection<CreateBookStepDto> BookSteps
    )
{
    public Book ToEntity()
    {
        return new Book
        {
            Title = this.Title,
            Isbn = this.Isbn,
            Nuart = this.Nuart,
            Pages = this.Pages ?? 0,
            Price = this.Price ?? 0,
            Note = this.Note,
            Summary = this.Summary,
            Hook = this.Hook,
            Marketing = this.Marketing,
            CoverFileName = this.CoverFileName,
            ReleaseDate = this.ReleaseDate,
        };
    }
}