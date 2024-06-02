using System.Numerics;

namespace ProductCatalogApp.Classes
{
    public static class GenericExtensions
    {
        /// <summary>Determines if a value represents an even integral value.</summary>
        public static bool IsEven<T>(this T sender) where T : INumber<T>
            => T.IsEvenInteger(sender);
    }
}
