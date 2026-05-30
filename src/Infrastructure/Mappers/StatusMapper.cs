using Infrastructure.Entities;
using DomainModels = Domain.Models;

namespace Infrastructure.Mappers;

public static class StatusMapper
{
    public static DomainModels.Status ToModel(this Status entity)
        => new(entity.Id, entity.Name);
}
