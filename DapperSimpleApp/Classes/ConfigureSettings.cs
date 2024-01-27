using System;
using System.IO;
using DapperSimpleApp.Models;
using Newtonsoft.Json;

namespace DapperSimpleApp.Classes
{
    /// <summary>
    /// Responsible for reading settings from appsettings.json
    /// </summary>
    /// <remarks>
    /// If ported to NET8+, use my NuGet package ConfigurationLibrary which is used in most of the
    /// projects in the solution.
    /// https://www.nuget.org/packages/ConfigurationLibrary/1.0.6?_src=template
    /// </remarks>
    public class ConfigureSettings
    {
        public static ApplicationSettings Get() 
            => JsonConvert.DeserializeObject<ApplicationSettings>(
                File.ReadAllText(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"appsettings.json")));

        public static string ConnectionString() 
            => Get().ConnectionString;
    }
}