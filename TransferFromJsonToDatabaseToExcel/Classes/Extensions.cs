using FastMember;
using System.Data;

namespace TransferFromJsonToDatabaseToExcel.Classes;
internal static class Extensions
{
    public static DataTable ToDataTable<T>(this IEnumerable<T> sender)
    {
        DataTable table = new(typeof(T).Name);
        using var reader = ObjectReader.Create(sender);
        table.Load(reader);
        return table;
    }
}

