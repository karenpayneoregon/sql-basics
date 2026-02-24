using Diacritics;
using Diacritics.AccentMappings;

namespace NorthWindSqlLiteApp1.Mappings;

/// <summary>
/// Represents a custom accent mapping for company names, providing replacements for specific accented characters.
/// </summary>
public class CompanyNameAccentsMapping : IAccentMapping
{
    private static readonly IDictionary<char, MappingReplacement> 
        MappingDictionary = new Dictionary<char, MappingReplacement> {
            { 'Ã', "a"},
            { '©', "e"},
            { '…', "" },
            { '³', "" },
            { '¼', "" },
            { '¡', "" },
            { '¶', "" },
            { '¤', "" },
            { '§', "" },
            { '¨', "" }
        };
    public IDictionary<char, MappingReplacement> Mapping => MappingDictionary;
}