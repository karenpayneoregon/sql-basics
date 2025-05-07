using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Classes;
public static class ResourceExtensions
{
    /// <summary>
    /// Retrieves a list of resource names that are images or icons from the specified <see cref="ResourceManager"/>.
    /// </summary>
    /// <param name="sender">The <see cref="ResourceManager"/> instance from which to retrieve the resource names.</param>
    /// <returns>A list of resource names that are images or icons, or <c>null</c> if an exception occurs.</returns>
    public static List<string> ResourceImageNames(this ResourceManager sender)
    {
        try
        {

            var names = new List<string>();
            var resourceSet = sender.GetResourceSet(CultureInfo.CurrentUICulture, true, true);

            names.AddRange(resourceSet!.Cast<DictionaryEntry>()
                .Where(dictionaryEntry => dictionaryEntry.Value is Image or Icon or byte[])
                .Select(dictionaryEntry => dictionaryEntry.Key.ToString())!);

            return names;

        }
        catch (Exception)
        {
            return null!;
        }
    }
}