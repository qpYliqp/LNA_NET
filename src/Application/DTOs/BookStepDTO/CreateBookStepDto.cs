using Domain.Entities;

namespace Application.DTOs.BookStepDTO;

public record CreateBookStepDto(
    int ProductionStepId,
    int StatusId, 
    DateTime EndDate
    )
{
    public BookStep ToEntity()
    {
        return new BookStep
        {
            ProductionStepId = this.ProductionStepId,
            StatusId = this.StatusId,
            EndDate = this.EndDate,
        };
    }
}