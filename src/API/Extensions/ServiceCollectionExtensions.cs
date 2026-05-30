using Amazon.S3;
using Domain.Configurations;
using Domain.Interfaces.Services;
using Domain.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IMinioInitializationService, MinioInitializationService>();
        services.AddScoped<IMinioService, MinioService>();
        services.AddScoped<IBookService, BookService>();

        // MediatR doit scanner l'assembly qui contient les handlers (API), pas le Domain.
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

        services.Configure<MinioSettings>(configuration.GetSection("MinioSettings"));

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

            return new AmazonS3Client(minioSettings.AccessKey, minioSettings.SecretKey, s3Config);
        });

        return services;
    }
}
