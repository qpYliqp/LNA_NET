using Amazon.S3;
using Amazon.S3.Model;
using Application.IServices;
using Microsoft.Extensions.Configuration;

namespace Application.ImplServices;

public class MinioInitializationService(IAmazonS3 s3Client, IConfiguration configuration) : IMinioInitializationService
{
    private readonly IAmazonS3 _s3Client = s3Client;
    private readonly IConfiguration _configuration = configuration;
    
    public async Task EnsureBucketsExistAsync()
    {
        IEnumerable<string> bucketNames = _configuration.GetSection("MinioSettings:Buckets").Get<string[]>() ?? Array.Empty<string>();        if (!bucketNames.Any())
        {
            return;
        }
        
        foreach (var bucketName in bucketNames)
        {
            await _s3Client.EnsureBucketExistsAsync(bucketName);
        }

    }
}