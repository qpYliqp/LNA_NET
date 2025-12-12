using Amazon.S3;
using Application.Configuration;
using Application.ImplServices;
using Application.IServices;
using Microsoft.Extensions.Options;

namespace API.Extensions;
using Domain.IRepositories;
using Data.ImplRepositories;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    //Étend le type IServiceCollection
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMinioInitializationService, MinioInitializationService>();
        services.AddScoped<IMinioService, MinioService>();
        services.AddScoped<IBookService, BookService>();
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(IMinioService).Assembly));
        
        var minioConfig = configuration.GetSection("MinioSettings");
        var endpointUrl = minioConfig["Endpoint"];
        var accessKey = minioConfig["AccessKey"];
        var secretKey = minioConfig["SecretKey"];
        
        services.Configure<MinioSettings>(
            configuration.GetSection("MinioSettings"));

        services.AddSingleton<IAmazonS3>(serviceProvider =>
        {
            var minioSettings = serviceProvider.GetRequiredService<IOptions<MinioSettings>>().Value;

            var s3Config = new AmazonS3Config
            {
                ServiceURL = minioSettings.Endpoint,
                ForcePathStyle = true,
                UseHttp = true,
                AuthenticationRegion = "us-east-1"
            };
            return new AmazonS3Client(
                minioSettings.AccessKey,
                minioSettings.SecretKey,
                s3Config
            );
        });
        
        return services;
    }
    
    
}