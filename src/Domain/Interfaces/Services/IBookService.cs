namespace Domain.Interfaces.Services;

public interface IBookService
{
    public Task<string?> GetBookCoverAsync(string? coverFileName);
}