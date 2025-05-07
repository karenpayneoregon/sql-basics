namespace WinFormsApp1.Classes;

/// <summary>
/// Specifies the type of image resource.
/// </summary>
/// <summary>
/// The image type is unknown or unsupported.
/// </summary>
/// <summary>
/// The image is a bitmap (BMP format).
/// </summary>
/// <summary>
/// The image is an icon (ICO format).
/// </summary>
/// <summary>
/// The image is in PNG format.
/// </summary>
/// <summary>
/// The image is in JPEG format.
/// </summary>
public enum ImageType
{
    Unknown,
    Bitmap,
    Icon,
    Png,
    Jpeg
}