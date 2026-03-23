# Migrating a Hello World sample to the AXSG (XAML Source Generator)

This short guide shows the minimal, practical steps to switch a small v11 app
to use the XamlToCSharpGenerator (AXSG) source-generated XAML backend. Two workflows
are described: using the published NuGet packages (recommended) and using a local
checkout of the AXSG repository for development.

## Prerequisites
- .NET 10 SDK
- v11 packages already referenced in the sample
- (optional) local checkout of https://github.com/wieslawsoltes/XamlToCSharpGenerator for development and debugging

## Overview
- Set the XAML compiler backend to `SourceGen` and enable the AXSG compiler switch.
- Add runtime integration (so extension methods like `UseAvaloniaSourceGeneratedXaml()` are available).
- Update `Program.cs` to opt-in the runtime loader.
- Replace or experiment with some XAML using C# expression bindings.

## Steps: use NuGet

0. Make a backup of your project before starting the migration, so you can easily revert if needed.

1. Add the AXSG package to the project:

```bash
dotnet add package XamlToCSharpGenerator
```

2. Update your project file (`csproj`) to select the SourceGen backend (minimal example):

```xml
<PropertyGroup>
	<AvaloniaXamlCompilerBackend>SourceGen</AvaloniaXamlCompilerBackend>
</PropertyGroup>

<ItemGroup>
	<PackageReference Include="XamlToCSharpGenerator" Version="*" />
</ItemGroup>
```

3. Update `Program.cs` with a call to `UseAvaloniaSourceGeneratedXaml()` to use the AXSG runtime Bootstrap extension:

```csharp
using XamlToCSharpGenerator.Runtime;

public static AppBuilder BuildAvaloniaApp() =>
		AppBuilder.Configure<App>()
				.UsePlatformDetect()
				.WithInterFont()
				.UseAvaloniaSourceGeneratedXaml()
				.LogToTrace();
```

4. Build and run:

```bash
dotnet build -c Debug
dotnet run --project src
```

## Features: using C# expressions in XAML
- AXSG supports explicit expressions using `{= ... }` and shorthand expression forms `{Name}` and interpolated forms `{$'...{expr}...'}`.
- Prefer single-quoted string literals inside expressions to avoid XML quoting headaches, e.g. `{= InputText != null ? InputText.ToUpper() : 'empty' }`.

> Note that this repo shows that in the csharp-expressions branch, and just some simple syntax. [A full specification is being developed by Microsoft](https://github.com/dotnet/maui/blob/main/docs/specs/XamlCSharpExpressions.md) for MAUI and AXSG will align with that spec as it evolves.

## Common troubleshooting
- Things might be missing or broken: see [CONTRIBUTE.md](CONTRIBUTE.md) for local-analyzer troubleshooting and fixes, and contribute detailed reports back to AXSG developers to help them improve the experience.
