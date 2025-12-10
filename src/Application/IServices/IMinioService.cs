using Application.Enumerations;

namespace Application.IServices;

public interface IMinioService
{
    public Task<string?> getFileUrlByNameAsync(string fileName, BucketNames bucketName);
}