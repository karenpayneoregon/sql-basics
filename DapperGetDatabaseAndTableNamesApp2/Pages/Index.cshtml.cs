using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Serilog;
using SqlServer.Library.Classes;
// ReSharper disable MethodHasAsyncOverload


namespace DapperGetDatabaseAndTableNamesApp2.Pages;
public class IndexModel : PageModel
{

    [BindProperty]

    public int Identifier { get; set; }
    public List<SelectListItem>? TableNames { get; set; }

    public async Task OnGet()
    {
        await GetTableNames();
    }

    private async Task GetTableNames()
    {
        var tables = await DataOperations.TableNamesWithIndicesAsync("NorthWind2024");
        ViewData["TableOptions"] = new SelectList(tables, "Id", "Name");
    }


    public async Task<PageResult> OnPostNormalExample()
    {
        var item = DataOperations.TableNamesWithIndices("NorthWind2024")
            .FirstOrDefault(x => x.Id == Identifier);

        Log.Information("Selected {P1}", item);

        await GetTableNames();

        return Page();

    }
}
