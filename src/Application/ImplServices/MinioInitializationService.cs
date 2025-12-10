using Amazon.S3;
using Amazon.S3.Model;
using Application.IServices;
using Microsoft.Extensions.Configuration;

namespace Application.ImplServices;
public record MinioBucket(string Name);

public class MinioInitializationService(IAmazonS3 s3Client, IConfiguration configuration) : IMinioInitializationService
{
    private readonly IAmazonS3 _s3Client = s3Client;
    private readonly IConfiguration _configuration = configuration;
    
    public async Task EnsureBucketsExistAsync()
    {
        var buckets = _configuration
            .GetSection("MinioSettings:Buckets")
            .Get<List<MinioBucket>>() ?? [];

        if (buckets.Count == 0)
            return;

        foreach (var bucket in buckets)
        {
            if (!string.IsNullOrWhiteSpace(bucket.Name))
            {
                await _s3Client.EnsureBucketExistsAsync(bucket.Name);
            }
        }
    }
}