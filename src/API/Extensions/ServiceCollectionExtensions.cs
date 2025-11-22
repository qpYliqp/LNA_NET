using Application.ImplServices;
using Application.IServices;

namespace API.Extensions;
using Domain.IRepositories;
using Data.ImplRepositories;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    //Étend le type IServiceCollection
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepositoryImpl>();
        services.AddScoped<IAuthorRepository, AuthorRepositoryImpl>();
        services.AddScoped<IBookService, BookService>();
        return services;
    }
}