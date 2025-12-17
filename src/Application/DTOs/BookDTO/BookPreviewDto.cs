using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Application.DTOs.BookDTO
{
    public record BookPreviewDto(
        int Id,
        string Title,
        string? CoverFileUrl
    );
}
