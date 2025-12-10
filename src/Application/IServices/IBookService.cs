namespace Application.IServices;

public interface IBookService
{
    public Task<string?> GetBookCoverAsync(string? coverFileName);
}