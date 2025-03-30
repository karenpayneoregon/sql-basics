using CategoriesApplication1.Data;
using CategoriesApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CategoriesApplication1.Pages;

public class IndexModel(Context context) : PageModel
{
    public required IList<Categories> Categories { get; set; }

    /// <summary>
    /// Handles GET requests for the Index page.
    /// </summary>
    /// <remarks>
    /// This method asynchronously retrieves a list of categories from the database
    /// and assigns it to the <see cref="Categories"/> property.
    /// </remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task OnGetAsync()
    {
        Categories = await context.Categories.ToListAsync();
    }
}
