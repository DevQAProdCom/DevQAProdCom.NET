# Dynamic Test & App Runner Setup
# Dual-Mode Test/Application Project

This project works as both an **NUnit Test Suite** and a **Console Application**, controlled by the `DebugAsApp` build configuration.

---

## How It Works

### The Problem
.NET projects with `Microsoft.NET.Test.Sdk` cannot have a `Main` entry point—it causes build conflicts.

### The Solution
`Program.cs` contains a standard `Main` method. An MSBuild target renames it in test builds:

- **App Mode (`DebugAsApp` configuration, `AsApp=true`):** Method stays named `Main` → recognized as entry point → executable created
- **Test Mode (default `Debug`/`Release`, `AsApp != true`):** MSBuild target renames `Main` to `Main_Not_As_App` → not recognized as entry point → tests run normally

---

## Project Structure

```
Tests.DevQAProdCom.NET.AI/
├── BuildTargets/RenameMainMethod.targets   # Renames method during test builds
├── Program.cs                              # Entry point with standard method name
├── AiTests.cs                              # Test classes
└── Tests.DevQAProdCom.NET.AI.csproj        # Controls mode switching
```

---

## Key Configuration

### `.csproj` Settings

```xml
<!-- Declare DebugAsApp as a valid configuration -->
<Configurations>Debug;Release;DebugAsApp</Configurations>

<!-- In DebugAsApp configuration, mark the project as an app -->
<PropertyGroup Condition="'$(Configuration)' == 'DebugAsApp'">
  <AsApp>true</AsApp>
  <DisableFastUpToDateCheck>true</DisableFastUpToDateCheck>
</PropertyGroup>

<!-- In non-app mode, this is a test project -->
<IsTestProject Condition="'$(AsApp)' != 'true'">true</IsTestProject>

<!-- In app mode, produce an executable -->
<OutputType Condition="'$(AsApp)' == 'true'">Exe</OutputType>

<!-- Exclude Test SDK in app mode (prevents conflicts) -->
<PackageReference Include="Microsoft.NET.Test.Sdk" Condition="'$(AsApp)' != 'true'" />

<!-- Import rename logic only in non-app mode -->
<Import Project="BuildTargets\RenameMainMethod.targets" Condition="'$(AsApp)' != 'true'" />
```

### `Program.cs` Entry Point

```csharp
static async Task Main(string[] args)
{
    // Uses reflection to invoke: <ClassName> <MethodName> [Parameters...]
    await ReflectionUtils.InvokeMethodWithArgsAsync(className, methodName, args, logger);
}
```

### `BuildTargets\RenameMainMethod.targets`

MSBuild target that:
1. Reads `Program.cs`
2. Replaces `static async Task Main(string[] args)` with `static async Task Main_Not_As_App(string[] args)`
3. Writes modified version to `obj/` directory
4. Compiles temp file instead of original

---

## Usage

### Run as NUnit Tests (Default)

```bash
dotnet test
```

- `Program.cs` is compiled with `Main` renamed to `Main_Not_As_App` → not an entry point
- NUnit test methods execute normally

### Run as Console Application

Always rebuild when switching to app mode (ensures RenameMainMethod.targets runs or is skipped correctly).

```bash
dotnet build Tests.DevQAProdCom.NET.AI --configuration DebugAsApp --force
dotnet run --project Tests.DevQAProdCom.NET.AI --configuration DebugAsApp --no-build -- TestsSuite TestName
```

```bash
dotnet build Tests.DevQAProdCom.NET.AI -p:AsApp=true --force
dotnet run --project Tests.DevQAProdCom.NET.AI -p:AsApp=true --no-build -- TestsSuite TestName
```

---

## Visual Studio

- To run tests: use **Test Explorer** with the default `Debug` configuration.
- To run as an app: set the active configuration to **`DebugAsApp`** and press `F5`.
