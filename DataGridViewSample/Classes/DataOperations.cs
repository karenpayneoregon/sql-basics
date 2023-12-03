using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using DataGridViewSample.Models;

namespace DataGridViewSample.Classes;

internal class DataOperations
{

    /// <summary>
    /// Get category for book
    /// </summary>
    /// <param name="book">Existing book</param>
    /// <remarks>
    /// For where the frontend does not always need a Book's category.
    /// </remarks>
    public static async Task GetCategory(Book book)
    {
        await using var cn = new SqlConnection(ConnectionString());
        SqlMapper.GridReader results = await cn.QueryMultipleAsync(SqlStatements.GetBookWithCategory, 
            new
            {
                book.CategoryId, 
                book.Id
            });

        Categories category = await results.ReadFirstAsync<Categories>();
        Book theBook = await results.ReadFirstAsync<Book>();
        
        book.Category = category;
    }

    public static async Task<Book> GetCategory(int id, int categoryId)
    {

        await using var cn = new SqlConnection(ConnectionString());
        SqlMapper.GridReader results = await cn.QueryMultipleAsync(SqlStatements.GetBookWithCategory,
            new
            {
                categoryId,
                id
            });

        Categories category = await results.ReadFirstAsync<Categories>();
        Book book = await results.ReadFirstAsync<Book>();
        book.Category = category;

        return book;
    }

    /// <summary>
    /// Get all books
    /// </summary>
    /// <returns>Books with category</returns>
    public static async Task<List<Book>> BooksAsync()
    {
        await using var cn = new SqlConnection(ConnectionString());
        IEnumerable<Book> books = await cn.QueryAsync<Book, Categories, Book>(SqlStatements.GetBooksWithCategories,
            (book, category) =>
            {
                book.Category = category;
                book.CategoryId = category.CategoryId;
                return book;
            }, splitOn: nameof(Book.CategoryId));

        return books.ToList();
    }

    public static List<Book> Books()
    {
        using var cn = new SqlConnection(ConnectionString());
        IEnumerable<Book> books =  cn.Query<Book, Categories, Book>(SqlStatements.GetBooksWithCategories,
            (book, category) =>
            {
                book.Category = category;
                book.CategoryId = category.CategoryId;
                return book;
            }, splitOn: nameof(Book.CategoryId));

        return books.ToList();
    }

}
