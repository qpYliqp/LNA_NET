using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.BookDTO
{
    public record BookLetterWithDetailsDto(
        int Id,
        string Title);
}
