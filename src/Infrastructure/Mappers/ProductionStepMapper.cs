using Infrastructure.Entities;
using DomainModels = Domain.Models;

namespace Infrastructure.Mappers;

public static class ProductionStepMapper
{
    public static DomainModels.ProductionStep ToModel(this ProductionStep entity)
        => new(entity.Id, entity.Name);
}
