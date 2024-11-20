using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations.Books;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public BookService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Book>> GetBooks()
    {
        var books = _applicationDbContext.Books.AsQueryable();

        books = books.Where(x => x.Title.Contains("Red"))
            .Where(x => x.PublishDate > new DateTime(2012, 5, 25));

        return await books.ToListAsync();
    }

    public async Task<Book> GetBook()
    {
        var books = _applicationDbContext.Books.AsQueryable();

        return await books.OrderByDescending(x => x.Price * x.QuantityPublished).FirstOrDefaultAsync();
    }
}
