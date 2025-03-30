namespace AlternateImageApp.Models;
#nullable disable
/// <summary>
/// Represents an image with associated alternate text and metadata.
/// </summary>
/// <remarks>
/// This class provides properties to manage image data, including its source path, 
/// alternate text.
/// </remarks>
public class ImageAltText
{
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the source path of the image.
    /// </summary>
    public string Src { get; set; }
    /// <summary>
    /// Gets or sets the byte array representing the image.
    /// </summary>
    public byte[] Photo { get; set; }
    /// <summary>
    /// Gets or sets the alternate text for the image.
    /// </summary>
    public string Alt { get; set; }
    public string Name => Path.GetFileNameWithoutExtension(Src);
    private string _ext;
    public string Ext
    {
        get => _ext ??= Path.GetExtension(Src);
        set => _ext = value;
    }
    public string FileName => Src;
}
