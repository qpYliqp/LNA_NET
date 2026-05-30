using Infrastructure.Entities;
using Myth.Interfaces;

namespace Infrastructure.Specifications;

public static class BookSpecification
{
    public static ISpec<Book> StartsWith(this ISpec<Book> spec, string? prefix)
    {
        return string.IsNullOrEmpty(prefix)
            ? spec
            : spec.And(b => b.Title.ToLower().StartsWith(prefix.ToLower()));
    }
}
