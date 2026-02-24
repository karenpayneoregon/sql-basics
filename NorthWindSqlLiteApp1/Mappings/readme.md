# Diacritics.NET

Diacritics.NET is a library for handling diacritics (accented characters) in .NET applications. It provides functionality for removing, replacing, and normalizing diacritics in strings.

## Features

- **Remove Diacritics**: Easily remove diacritics from strings.
- **Replace Diacritics**: Replace diacritics with their non-accented counterparts.
- **Normalize Strings**: Normalize strings by removing or replacing diacritics.

## Installation

You can install Diacritics.NET via NuGet:

```
dotnet add package Diacritics
```

## Usage

Here's a quick example of how to use Diacritics.NET in your application:

```csharp
using Diacritics.Extensions;

string text = "Café";
string noDiacritics = text.RemoveDiacritics();
```

## NorthWind custom mappings

- [City Name Accents Mapping](CityNameAccentsMapping.cs)
- [Company Name Accents Mapping](CompanyNameAccentsMapping.cs)