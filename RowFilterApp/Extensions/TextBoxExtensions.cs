namespace RowFilterApp.Extensions;
public static class TextBoxExtensions
{
    /// <summary>
    /// Method base of the function
    /// </summary>
    /// <param name="sender"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace(this TextBox sender) 
        => string.IsNullOrWhiteSpace(sender.Text);
}