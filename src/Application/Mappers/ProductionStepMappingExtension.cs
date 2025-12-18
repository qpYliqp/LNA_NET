using System.Linq.Expressions;
using Application.DTOs.ProductionStepDTO;
using Domain.Entities;

namespace Application.Mappers;

public static class ProductionStepMappingExtension
{
    private static Expression<Func<ProductionStep, ProductionStepDto>> ProjectToDto =>
        productionStep => new ProductionStepDto(
            Id : productionStep.Id,
            Name : productionStep.Name
        );

    public static ProductionStepDto ToDto(this ProductionStep productionStep)
    {
        return productionStep == null ? null : ProjectToDto.Compile()(productionStep);
    }
}