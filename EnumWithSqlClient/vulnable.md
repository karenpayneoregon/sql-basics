# NET8

Results from `dotnet list package --vulnerable --include-transitive`

Added the following to the project file

```xml
<PropertyGroup>
   <NuGetAudit>false</NuGetAudit>
</PropertyGroup>
```


The following sources were used:
   https://api.nuget.org/v3/index.json
   C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\
   C:\OED\NuGetLocal

Project `EnumWithSqlClient` has the following vulnerable packages
   [net8.0]: 
   Transitive Package                           Resolved   Severity   Advisory URL                                     
   > Azure.Identity                             1.10.3     Moderate   https://github.com/advisories/GHSA-wvxc-855f-jvrv
                                                           Moderate   https://github.com/advisories/GHSA-m5vv-6r4h-3vj9
   > Microsoft.Identity.Client                  4.56.0     Low        https://github.com/advisories/GHSA-x674-v45j-fwxw
                                                           Moderate   https://github.com/advisories/GHSA-m5vv-6r4h-3vj9
   > Microsoft.IdentityModel.JsonWebTokens      6.24.0     Moderate   https://github.com/advisories/GHSA-59j7-ghrg-fj52
   > System.Formats.Asn1                        5.0.0      High       https://github.com/advisories/GHSA-447r-wph3-92pm
   > System.IdentityModel.Tokens.Jwt            6.24.0     Moderate   https://github.com/advisories/GHSA-59j7-ghrg-fj52
   > System.Text.Json                           7.0.0      High       https://github.com/advisories/GHSA-hh2w-p6rv-4g7w


# NET9

```xml
<ItemGroup>
   <NuGetAuditSuppress Include="https://github.com/advisories/GHSA-hh2w-p6rv-4g7w" />
</ItemGroup>
```

The following sources were used:
   https://api.nuget.org/v3/index.json
   C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\
   C:\OED\NuGetLocal

Project `EnumWithSqlClient` has the following vulnerable packages
   [net9.0]: 
   Transitive Package      Resolved   Severity   Advisory URL                                     
   > System.Text.Json      7.0.0      High       https://github.com/advisories/GHSA-hh2w-p6rv-4g7w