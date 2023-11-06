namespace DapperWithExcel.Classes;
internal class Utilities
{
    public static T ConvertFromObject<T>(object sender) 
        => (T)Enum.Parse(typeof(T), sender.ToString()!);
}
