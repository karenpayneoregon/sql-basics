using System.Data;
using Microsoft.Data.SqlClient;
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


    public static async Task<List<Book>> BooksAsync()
    {
        await using var cn = new SqlConnection(ConnectionString());

        return (await cn.QueryAsync<Book, Categories, Book>(SqlStatements.GetBooksWithCategories,
            (book, category) =>
            {
                book.Category = category;
                book.CategoryId = category.CategoryId;
                return book;
            }, splitOn: nameof(Book.CategoryId)))
            .AsList();
    }

    public static List<Book> Books()
    {
        using var cn = new SqlConnection(ConnectionString());

        return (cn.Query<Book, Categories, Book>(SqlStatements.GetBooksWithCategories,
            (book, category) =>
            {
                book.Category = category;
                book.CategoryId = category.CategoryId;
                return book;
            }, splitOn: nameof(Book.CategoryId)))
            .AsList();
    }

    public static List<Book> Books1()
    {
        using var cn = new SqlConnection(ConnectionString());

        return (cn.Query<Book, Categories, Book>("usp_GetBooksWithCategories",
                (book, category) =>
                {
                    book.Category = category;
                    book.CategoryId = category.CategoryId;
                    return book;
                }, splitOn: nameof(Book.CategoryId), commandType: CommandType.StoredProcedure))
            .AsList();
    }

}
