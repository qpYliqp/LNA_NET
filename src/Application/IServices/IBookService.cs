using Application.DTOs.BookDTO;
using Application.DTOs.BookStepDTO;
using Domain.Entities;

namespace Application.IServices;

public interface IBookService
{
    public Task<string?> GetBookCoverAsync(string? coverFileName);
    
    public void ConfigureBookSteps(Book book, ICollection<CreateBookStepDto> bookSteps);
}