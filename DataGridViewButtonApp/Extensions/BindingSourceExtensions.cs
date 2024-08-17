using System.Data;

namespace DataGridViewButtonApp.Extensions;

public static class BindingSourceExtensions
{
    public static DataTable DataTable(this BindingSource pBindingSource) => (DataTable)pBindingSource.DataSource;

    public static DataRow CurrentRow(this BindingSource pBindingSource) => ((DataRowView)pBindingSource.Current).Row;
}