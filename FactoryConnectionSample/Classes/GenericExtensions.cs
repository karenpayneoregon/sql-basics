using System.Numerics;

namespace FactoryConnectionSample.Classes
{
    public static class GenericExtensions
    {
        public static bool IsOdd<T>(this T sender) where T : INumber<T>
            => T.IsOddInteger(sender);
    }
}
