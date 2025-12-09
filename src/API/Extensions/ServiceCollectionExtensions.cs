using Amazon.S3;
using Application.ImplServices;
using Application.IServices;

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
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(IBookService).Assembly));
        
        var minioConfig = configuration.GetSection("MinioSettings");
        var endpointUrl = minioConfig["Endpoint"];
        var accessKey = minioConfig["AccessKey"];
        var secretKey = minioConfig["SecretKey"];

        var s3Config = new AmazonS3Config
        {
            ServiceURL = endpointUrl,
            //Permet d'adresser directement les buckets
            //Permet au sdk C# de génèrer l'URL dans le bon format
            ForcePathStyle = true,
            UseHttp = true,
            AuthenticationRegion = "es-east-1"
        };
        services.AddSingleton<IAmazonS3>(sp =>
            new AmazonS3Client(accessKey, secretKey, s3Config));
        
        return services;
    }
    
    
}