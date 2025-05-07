using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1.Models;
/// <summary>
/// Represents a resource item that can be either a bitmap or an icon.
/// </summary>
public class ResourceItem
{
    /// <summary>
    /// Resource name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Image which is either an icon or bitmap
    /// </summary>
    public Bitmap? Image { get; set; }

    public Icon Icon { get; set; }
    /// <summary>
    /// Indicates if dealing with an icon so when displaying the
    /// control used to display can adjust it's size or Size mode
    /// </summary>
    public bool IsIcon { get; set; }

    public override string ToString() => Name;
}