namespace DataGridViewButtonApp.Extensions;

public static class GenericExtensions
{
    public static bool IsA<T>(this object pObject) => pObject is T;
}