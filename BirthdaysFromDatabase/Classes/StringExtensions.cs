namespace BirthdaysFromDatabase.Classes;
public static class StringExtensions
{
    public static string SplitCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        Span<char> result = stackalloc char[input.Length * 2];
        var resultIndex = 0;

        for (var index = 0; index < input.Length; index++)
        {
            var currentChar = input[index];

            if (index > 0 && char.IsUpper(currentChar))
            {
                result[resultIndex++] = ' ';
            }

            result[resultIndex++] = currentChar;
        }

        return result[..resultIndex].ToString();

    }
}
