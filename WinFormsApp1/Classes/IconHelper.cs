namespace WinFormsApp1.Classes;

public class IconHelper
{
    /// <summary>
    /// Converts a byte array to an <see cref="Icon"/> object.
    /// </summary>
    /// <param name="iconBytes">The byte array representing the icon data.</param>
    /// <returns>An <see cref="Icon"/> object created from the provided byte array.</returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="iconBytes"/> is <c>null</c> or empty.
    /// </exception>
    public static Icon ByteArrayToIcon(byte[] iconBytes)
    {
        if (iconBytes == null || iconBytes.Length == 0)
            throw new ArgumentException("Icon byte array is null or empty.");

        using MemoryStream ms = new(iconBytes);
        return new Icon(ms);
    }
}