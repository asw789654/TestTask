using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations.Authors;

public class AuthorService : IAuthorService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AuthorService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Author>> GetAuthors()
    {
        var books = _applicationDbContext.Books.AsQueryable();

        var authors = _applicationDbContext.Authors.AsQueryable();

        var authorsDict = await books.Where(x => x.PublishDate.Year > 2015 && x.Author.Books.Count() % 2 == 0)
            .GroupBy(y => y.Author)
            .ToDictionaryAsync(key => key.Key, value => value.ToList());

        return authorsDict.Keys.ToList();

    }

    public async Task<Author> GetAuthor()
    {
        var authors = _applicationDbContext.Authors.AsQueryable();

        var books = _applicationDbContext.Books.AsQueryable();

        var maxLen = await books.MaxAsync(x => x.Title.Length);

        var book = await books.OrderByDescending(y => y.AuthorId).FirstOrDefaultAsync(x => x.Title.Length == maxLen);

        return await authors.FirstOrDefaultAsync(x => x.Id == book.AuthorId);
    }
}
