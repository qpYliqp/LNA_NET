using Infrastructure.Entities;
using DomainModels = Domain.Models;

namespace Infrastructure.Mappers;

public static class AuthorMapper
{
    public static DomainModels.Author ToModel(this Author entity)
        => new(entity.Id, entity.Name);
}
