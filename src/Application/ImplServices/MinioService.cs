using Amazon.S3;
using Amazon.S3.Model;
using Application.Configuration;
using Application.Enumerations;
using Application.IServices;
using Microsoft.Extensions.Options;


namespace Application.ImplServices;
public class MinioService(IAmazonS3 s3Client,IOptions<MinioSettings> minioSettings) : IMinioService
{
    private readonly IAmazonS3 _s3Client =  s3Client;
    private readonly MinioSettings _minioSettings = minioSettings.Value;
    

    public Task<string?> getFileUrlByNameAsync(string fileName, BucketNames bName)
    {
        string bucketKey = bName.ToString().ToLower();

        string? bucketName = _minioSettings.Buckets
            .FirstOrDefault(b => b.Name.Equals(bucketKey, StringComparison.OrdinalIgnoreCase))
            ?.Name; 

        if (string.IsNullOrEmpty(bucketName))
        {
            throw new InvalidOperationException($"Le nom de bucket '{bucketKey}' n'a pas été trouvé dans la configuration MinioSettings.");
        }

        GetPreSignedUrlRequest request = new GetPreSignedUrlRequest
        {
            BucketName = bucketName,
            Key = fileName, 
            Expires = DateTime.Now.AddHours(24)
        };
        
        string url = _s3Client.GetPreSignedURL(request);

        return Task.FromResult<string?>(url);
    }
}