using Domain.Models;

namespace API.Features.Books.Commands.UpdateBook;

/// <summary>
/// Contrat d'entrée HTTP pour la mise à jour d'un livre.
/// </summary>
public record UpdateBookRequest(
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
    ICollection<int> AuthorsId,
    ICollection<UpdateBookStepRequest> BookSteps)
{
    public Book ToModel() => new()
    {
        Id = Id,
        Title = Title,
        Isbn = Isbn,
        Nuart = Nuart,
        Pages = Pages,
        Price = Price,
        Note = Note,
        Summary = Summary,
        Hook = Hook,
        Marketing = Marketing,
        CoverFileName = CoverFileName,
        ReleaseDate = ReleaseDate
    };
}

public record UpdateBookStepRequest(int Id, int ProductionStepId, int StatusId, DateTime EndDate)
{
    public BookStep ToModel() => new()
    {
        Id = Id,
        ProductionStepId = ProductionStepId,
        StatusId = StatusId,
        EndDate = EndDate
    };
}
