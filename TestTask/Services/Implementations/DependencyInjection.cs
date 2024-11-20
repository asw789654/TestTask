using TestTask.Services.Implementations.Authors;
using TestTask.Services.Implementations.Books;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IBookService, BookService>()
            .AddTransient<IAuthorService, AuthorService>();
    }
}