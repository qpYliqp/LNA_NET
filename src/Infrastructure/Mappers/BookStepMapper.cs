using Infrastructure.Entities;
using DomainModels = Domain.Models;

namespace Infrastructure.Mappers;

public static class BookStepMapper
{
    public static DomainModels.BookStep ToModel(this BookStep entity)
        => new()
        {
            Id = entity.Id,
            ProductionStepId = entity.ProductionStepId,
            StatusId = entity.StatusId,
            EndDate = entity.EndDate
        };

    public static BookStep ToEntity(this DomainModels.BookStep model)
        => new()
        {
            Id = model.Id,
            ProductionStepId = model.ProductionStepId,
            StatusId = model.StatusId,
            EndDate = model.EndDate
        };
}
