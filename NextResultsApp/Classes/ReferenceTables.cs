using GitHubSamples.Models;

namespace GitHubSamples.Classes;

public class ReferenceTables
{
    public List<Categories> CategoriesList { get; set; } = new List<Categories>();
    public List<ContactType> ContactTypesList { get; set; } = new List<ContactType>();
    public List<Countries> CountriesList { get; set; } = new List<Countries>();
}