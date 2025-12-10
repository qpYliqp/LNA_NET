using Application.Enumerations;
using Application.IServices;

namespace Application.ImplServices;

public class BookService(IMinioService minioService) : IBookService
{
    private readonly IMinioService _minioService = minioService;
    public async Task<string?> GetBookCoverAsync(string? coverFileName)
    {
        string? coverUrl = null;
        if (coverFileName != null)
        {
            coverUrl = await _minioService.GetFileUrlByNameAsync(coverFileName, BucketNames.Cover);
        }
        return coverUrl;
    }
    
}