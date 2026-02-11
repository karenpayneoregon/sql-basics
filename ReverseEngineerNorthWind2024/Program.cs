using ReverseEngineerNorthWind2024.Classes.Core;
using Spectre.Console;

namespace ReverseEngineerNorthWind2024;
internal partial class Program
{
    static void Main(string[] args)
    {
        SpectreConsoleHelpers.PinkPill(Justify.Left, "Hello world");
        SpectreConsoleHelpers.ExitPrompt(Justify.Left);
    }
}
