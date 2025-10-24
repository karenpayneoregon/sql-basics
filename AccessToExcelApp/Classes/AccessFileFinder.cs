using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;

namespace AccessToExcelApp.Classes;
public class AccessFileFinder
{
    public static (List<string> MdbFiles, List<string> AccdbFiles)  GetAccessFiles(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");

        var directoryInfo = new DirectoryInfo(directoryPath);
        var directoryWrapper = new DirectoryInfoWrapper(directoryInfo);

        // Globbing matcher
        var matcher = new Matcher(StringComparison.OrdinalIgnoreCase);

        // Match patterns
        matcher.AddInclude("**/*.mdb");
        matcher.AddInclude("**/*.accdb");

        var matches = matcher.Execute(directoryWrapper);

        // Split into two lists
        var mdbFiles = new List<string>();
        var accdbFiles = new List<string>();

        foreach (var match in matches.Files)
        {
            var filePath = Path.Combine(directoryPath, match.Path);
            if (filePath.EndsWith(".mdb", StringComparison.OrdinalIgnoreCase))
                mdbFiles.Add(filePath);
            else if (filePath.EndsWith(".accdb", StringComparison.OrdinalIgnoreCase))
                accdbFiles.Add(filePath);
        }

        return (mdbFiles, accdbFiles);
    }
}