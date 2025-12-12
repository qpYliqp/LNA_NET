using Application.Enumerations;

namespace Application.IServices;

public interface IMinioService
{
    public Task<string?> GetFileUrlByNameAsync(string fileName, BucketNames bucketName);
}