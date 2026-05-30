using Amazon.S3;
using Amazon.S3.Model;
using Domain.Configurations;
using Domain.Enumerations;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Options;

namespace Domain.Services;

public class MinioService(IAmazonS3 s3Client, IOptions<MinioSettings> minioSettings) : IMinioService
{
    private readonly IAmazonS3 _s3Client = s3Client;
    private readonly MinioSettings _minioSettings = minioSettings.Value;

    public async Task<string?> GetFileUrlByNameAsync(string fileName, BucketNames bName)
    {
        var bucketName = ResolveBucketName(bName);
        try
        {
            await _s3Client.GetObjectMetadataAsync(new GetObjectMetadataRequest
            {
                BucketName = bucketName,
                Key = fileName
            });

            return await _s3Client.GetPreSignedURLAsync(new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = fileName,
                Expires = DateTime.Now.AddHours(24),
                Protocol = Protocol.HTTP
            });
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<string> UploadFileAsync(Stream content, string fileName, string contentType, BucketNames bName)
    {
        var bucketName = ResolveBucketName(bName);

        await _s3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = bucketName,
            Key = fileName,
            InputStream = content,
            ContentType = contentType,
            AutoCloseStream = false,
            DisablePayloadSigning = true // requis par MinIO en HTTP
        });

        return fileName;
    }

    private string ResolveBucketName(BucketNames bName)
    {
        var bucketKey = bName.ToString().ToLower();
        return _minioSettings.Buckets
                   .FirstOrDefault(b => b.Name.Equals(bucketKey, StringComparison.OrdinalIgnoreCase))
                   ?.Name
               ?? throw new InvalidOperationException(
                   $"Le nom de bucket '{bucketKey}' n'a pas été trouvé dans la configuration MinioSettings.");
    }
}
