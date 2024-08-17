using System.Data;

namespace DataGridViewButtonApp.Extensions;

public static class BindingSourceExtensions
{
    public static DataTable DataTable(this BindingSource pBindingSource)
    {
        return (DataTable)pBindingSource.DataSource;
    }
    public static DataRow CurrentRow(this BindingSource pBindingSource)
    {
        return ((DataRowView)pBindingSource.Current).Row;
    }
}