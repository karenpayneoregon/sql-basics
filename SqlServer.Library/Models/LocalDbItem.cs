#nullable disable


namespace SqlServer.Library.Models;
public class LocalDbItem
{
    public string Name { get; set; }
    public Version Version { get; set; }
    public string SharedName { get; set; }
    public string Owner { get; set; }
    public bool AutoCreate { get; set; }
    public DateTime LastStartTime { get; set; }
    public string State { get; set; }
}
