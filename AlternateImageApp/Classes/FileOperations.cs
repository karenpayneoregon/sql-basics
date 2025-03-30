using System.Text.RegularExpressions;
using AlternateImageApp.Models;

namespace AlternateImageApp.Classes;

/// <summary>
/// Provides utility methods for handling file operations related to image alternate text data.
/// There is no error handling but feel free to add error handling in.
/// </summary>
/// <remarks>
/// This class includes functionality to read and parse image alternate text entries from files.
/// It utilizes regular expressions to extract relevant data from file contents.
/// </remarks>
public partial class FileOperations
{
    /// <summary>
    /// Reads a list of image alternate text entries from a specified file.
    /// </summary>
    /// <param name="fileName">The path to the file containing image alternate text data.</param>
    /// <returns>A list of <see cref="ImageAltText"/> objects parsed from the file.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="fileName"/> is null.</exception>
    /// <exception cref="FileNotFoundException">Thrown if the specified file does not exist.</exception>
    /// <exception cref="IOException">Thrown if an I/O error occurs while reading the file.</exception>
    public static List<ImageAltText> ReadAltTexts(string fileName) =>
    (
        from line in File.ReadLines(fileName) 
        select ImageAltTextRegex().Match(line) into match 
        where match.Success select new ImageAltText
        {
            Src = match.Groups[1].Value, 
            Alt = match.Groups[2].Value
        }).ToList();


    [GeneratedRegex(@"<img\s+src=""([^""]+)""\s+alt=""([^""]+)""\s*/?>", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex ImageAltTextRegex();
}