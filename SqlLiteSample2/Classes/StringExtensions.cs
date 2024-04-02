using System.Text;

namespace SqlLiteSample2.Classes;
public static partial class StringExtensions
{
    /// <summary>
    /// Splits pascal case, so "FooBar" would become "Foo Bar".
    /// </summary>
    /// <remarks>
    /// Pascal case strings with periods delimiting the upper case letters,
    /// such as "Address.Line1", will have the periods removed.
    /// </remarks>
    public static string SplitPascalCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var retVal = new StringBuilder(input.Length + 5);

        for (int index = 0; index < input.Length; ++index)
        {
            var currentChar = input[index];
            if (char.IsUpper(currentChar))
            {

                if ((index > 1 && !char.IsUpper(input[index - 1])) 
                    || (index + 1 < input.Length && !char.IsUpper(input[index + 1])))
                {
                    retVal.Append(' ');
                }

            }

            if (!Equals('.', currentChar) || index + 1 == input.Length || !char.IsUpper(input[index + 1]))
            {
                retVal.Append(currentChar);
            }
        }

        return retVal.ToString().Trim();
    }
}
