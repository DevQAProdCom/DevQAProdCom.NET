# Dynamic Test & App Runner Setup
# Dual-Mode Test/Application Project

This project works as both an **NUnit Test Suite** and a **Console Application**, controlled by the `AsApp` build property.

---

## How It Works

### The Problem
.NET projects with `Microsoft.NET.Test.Sdk` cannot have a `Main` entry point—it causes build conflicts.

### The Solution
We use a **long method name** that MSBuild renames during app builds:

- **Test Mode (default):** Method is named `Main_ForApp_With_This_Long_Name_Is_Changed_To_Just_Main_During_Build_AsApp` → not recognized as entry point → tests run normally
- **App Mode (`AsApp=true`):** MSBuild target renames it to `Main` → becomes entry point → executable created

---

## Project Structure

```
Tests.DevQAProdCom.NET.AI/
├── Build/RenameMainMethod.targets   # Renames method during build
├── Program.cs                       # Entry point with long method name
├── AiTests.cs                       # Test classes
└── Tests.DevQAProdCom.NET.AI.csproj # Controls mode switching
```

---

## Key Configuration

### `.csproj` Settings

```xml
<!-- Switch between test/app mode -->
<IsTestProject Condition="'$(AsApp)' == 'true'">false</IsTestProject>
<OutputType Condition="'$(AsApp)' == 'true'">Exe</OutputType>

<!-- Exclude Test SDK in app mode (prevents conflicts) -->
<PackageReference Include="Microsoft.NET.Test.Sdk" Condition="'$(AsApp)' != 'true'" />

<!-- Import rename logic only when needed -->
<Import Project="Build\RenameMainMethod.targets" Condition="'$(AsApp)' == 'true'" />
```

### `Program.cs` Entry Point

```csharp
static void Main_ForApp_With_This_Long_Name_Is_Changed_To_Just_Main_During_Build_AsApp(string[] args)
{
    // Uses reflection to invoke: <ClassName> <MethodName> [Parameters...]
    ReflectionUtils.InvokeMethodWithArgs(className, methodName, args, logger);
}
```

### `Build\RenameMainMethod.targets`

MSBuild target that:
1. Reads `Program.cs`
2. Replaces long method name with `Main`
3. Writes temp file to `obj/` directory
4. Compiles temp file instead of original

---

## Usage

### Run as NUnit Tests (Default)

```bash
dotnet test
```

- `Program.cs` is compiled but ignored (long method name ≠ entry point)
- NUnit test methods execute normally

### Run as Console Application
Always rebuild when switching to app mode (ensures RenameMainMethod.targets runs)
```bash
dotnet build Tests.DevQAProdCom.NET.AI -p:AsApp=true --force
dotnet run --project Tests.DevQAProdCom.NET.AI -p:AsApp=true --no-build -- TestsSuite TestName
```