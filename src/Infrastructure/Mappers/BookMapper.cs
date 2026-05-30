using Infrastructure.Entities;
using DomainModels = Domain.Models;

namespace Infrastructure.Mappers;

public static class BookMapper
{
    public static DomainModels.Book ToModel(this Book entity)
        => new()
        {
            Id = entity.Id,
            Title = entity.Title,
            Isbn = entity.Isbn,
            Nuart = entity.Nuart,
            Pages = entity.Pages,
            Price = entity.Price,
            GlobalStatusId = entity.GlobalStatusId,
            Note = entity.Note,
            Summary = entity.Summary,
            Hook = entity.Hook,
            Marketing = entity.Marketing,
            CoverFileName = entity.CoverFileName,
            ReleaseDate = entity.ReleaseDate,
            Authors = entity.Authors.Select(a => a.ToModel()).ToList(),
            BookSteps = entity.BookSteps.Select(s => s.ToModel()).ToList()
        };

    /// <summary>
    /// Construit l'entité de persistance à partir du modèle de domaine.
    /// Les auteurs (relation many-to-many) sont attachés séparément par le handler
    /// à partir d'entités suivies par le contexte.
    /// </summary>
    public static Book ToEntity(this DomainModels.Book model)
        => new()
        {
            Id = model.Id,
            Title = model.Title,
            Isbn = model.Isbn,
            Nuart = model.Nuart,
            Pages = model.Pages,
            Price = model.Price,
            GlobalStatusId = model.GlobalStatusId,
            Note = model.Note,
            Summary = model.Summary,
            Hook = model.Hook,
            Marketing = model.Marketing,
            CoverFileName = model.CoverFileName,
            ReleaseDate = model.ReleaseDate,
            BookSteps = model.BookSteps.Select(s => s.ToEntity()).ToList()
        };
}
