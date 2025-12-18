using System.Linq.Expressions;
using Application.DTOs.StatusDTO;
using Domain.Entities;

namespace Application.Mappers;

public static class StatusMappingExtension
{
    private static Expression<Func<Status, StatusDto>> ProjectToDto =>
        status => new StatusDto(
            Id : status.Id,
            Name : status.Name
        );

    public static StatusDto ToDto(this Status status)
    {
        return status == null ? null : ProjectToDto.Compile()(status);
    }
}