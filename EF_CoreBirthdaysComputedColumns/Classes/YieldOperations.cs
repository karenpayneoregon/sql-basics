using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_CoreBirthdaysComputedColumns.Data;
using EF_CoreBirthdaysComputedColumns.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_CoreBirthdaysComputedColumns.Classes;
internal class YieldOperations
{
    public static async IAsyncEnumerable<BirthDays> AgeExample()
    {
        await using var context = new Context();
        var list = await context.BirthDays.ToListAsync();
        foreach (var bd in list)
        {
            if (bd.YearsOld < 60)
            {
                yield return bd;
            }
            else
            {
                yield break;
            }
        }

    }
}
