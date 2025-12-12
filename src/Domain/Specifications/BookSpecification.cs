using Domain.Entities;
using Myth.Interfaces;
using Myth.Specifications;
namespace Domain.Specifications;

public static class BookSpecification
{
    public static ISpec<Book> StartsWith( this ISpec<Book> spec, string prefix ) {
        if (string.IsNullOrEmpty(prefix))
        {
            return spec;
        }
        return spec.And(b => b.Title.ToLower().StartsWith(prefix.ToLower()));
    }
    
    
}