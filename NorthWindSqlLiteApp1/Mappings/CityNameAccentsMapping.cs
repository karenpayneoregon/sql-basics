using Diacritics.AccentMappings;
using Diacritics;

namespace NorthWindSqlLiteApp1.Mappings;

/// <summary>
/// Represents a mapping of city name accents to their replacements, implementing the <see cref="IAccentMapping"/> interface.
/// </summary>
/// <remarks>
/// This class provides a predefined dictionary of character mappings, where specific accented characters 
/// are replaced with their corresponding replacements. It is primarily used to normalize city names by removing 
/// or replacing diacritical marks.
/// </remarks>
public class CityNameAccentsMapping : IAccentMapping
{
  
    private static readonly IDictionary<char, MappingReplacement> 
        MappingDictionary = new Dictionary<char, MappingReplacement> {
        { 'Ã', "" },
        { '©', "e" },
        { '…', "" },
        { '³', "" },
        { '¼', "" },
        { '¥', "" },
        { '¶', "" },
        { '¤', "" },
        { '¨', "" }
    };
    
    /// <summary>
    /// Gets the dictionary of character mappings used for replacing accented characters 
    /// with their corresponding replacements.
    /// </summary>
    /// <value>
    /// A dictionary where the key is an accented character, and the value is the 
    /// <see cref="MappingReplacement"/> representing the replacement for that character.
    /// </value>
    /// <remarks>
    /// This property provides access to the predefined mappings used to normalize city names by removing 
    /// or replacing diacritical marks. It is a core component of the <see cref="CityNameAccentsMapping"/> class.
    /// </remarks>
    public IDictionary<char, MappingReplacement> Mapping => MappingDictionary;
}
