# About

## Current code

Microsoft example for a starter to conceal text in a database is shown below.

```csharp
modelBuilder.Entity<User>().Property(e => e.Password).HasConversion(
    v => new string(v.Reverse().ToArray()),
    v => new string(v.Reverse().ToArray()));
```

First iteration is shown in `Original conversion`.

This iteration uses NuGet package [BCrypt.Net.BCrypt](https://www.nuget.org/packages/BCrypt.Net-Next/4.0.3?_src=template).

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>().Property(e => e.Password).HasConversion(
        v => BC.HashPassword(v),
        v => v);
}
```

A user enters a password which `BC.HashPassword` which hashes the user’s password. Then for instance in a login process a password is entered and verified using `BC.Verify` by passing in as parameter 1 the entered password and for the second parameter, the hashed password from the database which returns a Boolean indicating if the password matches the hashed password from the database.

## BC 

Is an alias defined in the project file for `BCrypt.Net.BCrypt` package.

```xml
<ItemGroup>
   <PackageReference Include="BCrypt.Net-Next" Version="4.0.3" />
   <Using Include="BCrypt.Net.BCrypt" Alias="BC" />
</ItemGroup>
```

---

## Original code
[Microsoft source](https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Modeling/ValueConversions/EncryptPropertyValues.cs)

Simple starter code sample for `HasConversion` which in this case takes a string and reverses the string and stores in the table then read back reverses the value back to what was entered.

**Original conversion**

Which is great as it allows a developer to work from a simple example to implement their own logic

```csharp
modelBuilder.Entity<User>().Property(e => e.Password).HasConversion(
    v => new string(v.Reverse().ToArray()),
    v => new string(v.Reverse().ToArray()));
```

**Current conversion**

Sample to go passed the above.

```csharp
modelBuilder.Entity<User>().Property(e => e.Password).HasConversion(
    v => EncryptString(v),
    v => DecryptString(v));
```