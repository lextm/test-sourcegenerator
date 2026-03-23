# Notes to Contributors

This document contains development-focused instructions for iterating on the XamlToCSharpGenerator (AXSG) locally. It is intended for developers debugging the generator and testing it against a more complex sample.

## Local development against AXSG repository (dev-only)

Use this when you want to iterate on the generator itself and test changes locally.

1. Clone or have the local checkout of the AXSG repository available, e.g.
   `/Users/lextm/vscode-axaml/src/XamlToCSharpGenerator`.

2. Wire the build-time props/targets into your sample project (dev convenience):

```xml
<!-- conditionally import buildTransitive props/targets from local checkout -->
<Import Project="../path/to/XamlToCSharpGenerator/src/XamlToCSharpGenerator.Build/buildTransitive/XamlToCSharpGenerator.Build.props"
                Condition="Exists('../path/to/XamlToCSharpGenerator/src/XamlToCSharpGenerator.Build/buildTransitive/XamlToCSharpGenerator.Build.props')" />

<!-- set the backend and point at the local analyzer project for analyzer orchestration -->
<PropertyGroup>
    <AvaloniaXamlCompilerBackend>SourceGen</AvaloniaXamlCompilerBackend>
    <AvaloniaSourceGenCompilerEnabled>true</AvaloniaSourceGenCompilerEnabled>
    <XamlSourceGenLocalAnalyzerProject>../path/to/XamlToCSharpGenerator/src/XamlToCSharpGenerator.Generator/XamlToCSharpGenerator.Generator.csproj</XamlSourceGenLocalAnalyzerProject>
</PropertyGroup>
```

3. Build the AXSG repo (so analyzer DLLs exist):

```bash
cd /path/to/XamlToCSharpGenerator
dotnet restore
dotnet build -c Debug
```

4. (Optional) Add direct `Analyzer` items to the sample project to point to the built generator DLLs (helpful during dev):

```xml
<ItemGroup Condition="Exists('../path/to/XamlToCSharpGenerator/src/XamlToCSharpGenerator.Generator/bin/Debug/netstandard2.0/XamlToCSharpGenerator.Generator.dll')">
    <Analyzer Include="../path/to/XamlToCSharpGenerator/src/XamlToCSharpGenerator.Generator/bin/Debug/netstandard2.0/XamlToCSharpGenerator.Generator.dll" />
    <!-- add other required dependency DLLs from the local build output as needed -->
</ItemGroup>
```

5. Add a `ProjectReference` to the runtime helper (so `UseAvaloniaSourceGeneratedXaml` is available):

```xml
<ItemGroup>
    <ProjectReference Include="../path/to/XamlToCSharpGenerator/src/XamlToCSharpGenerator.Runtime.Avalonia/XamlToCSharpGenerator.Runtime.Avalonia.csproj" />
</ItemGroup>
```

6. Build the sample. If everything is wired correctly, the generator should run and produce the generated partials (e.g., `InitializeComponent` will be available at compile time):

```bash
dotnet build -c Debug
```

Optional: During active generator development it can be helpful to pack the local generator into a local nupkg and reference that from the sample for a more stable dev loop.

Troubleshooting (local-analyzer specific)
- `InitializeComponent` missing is often caused by the source generator not running because the analyzer failed to load (TargetInvocationException). If you see analyzer load errors:
  - Prefer using the published NuGet `XamlToCSharpGenerator` package while debugging loader issues.
  - If using the local checkout, ensure you built the generator and all its dependency projects first and add all required DLLs as `Analyzer` entries or use `XamlSourceGenLocalAnalyzerProject` + import the build transitive props/targets.
  - Run the build with detailed logging to capture analyzer exceptions:

```bash
dotnet build -c Debug -v:detailed
```

If you prefer a stable dev loop, pack the local generator into a local nupkg and reference it from the sample.
