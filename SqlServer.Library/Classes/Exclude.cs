namespace SqlServer.Library.Classes;
public class Exclude
{
    /// <summary>
    /// Database names to exclude
    /// </summary>
    public static List<string> DatabaseNameList => ["master", "msdb"];
    /// <summary>
    /// Table names to exclude
    /// </summary>
    public static List<string> TableNameList => ["sysdiagrams"];
}
