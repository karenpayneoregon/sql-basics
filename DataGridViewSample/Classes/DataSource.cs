using DataGridViewSample.Models;
using System.ComponentModel;

namespace DataGridViewSample.Classes;
public sealed class DataSource
{
    private static readonly Lazy<DataSource> Lazy = 
        new(() => new DataSource());

    public static DataSource Instance => Lazy.Value;

    public readonly BindingSource BindingSource = new();
    public BindingList<Book> BindingList;

    private DataSource()
    {
        BindingList = new BindingList<Book>(DataOperations.Books1());
        BindingSource.DataSource = BindingList;
    }
}
