# Dynamic Test & App Runner Setup

This project is configured to work as both an **NUnit Test Suite** and a **Runnable Console Application**. By default, it acts as a test project, but it can be toggled into an application via build properties or command-line parameters.

---

## Primary Setup: Custom MSBuild Property (Recommended)

This approach dynamically toggles the project type during compilation using the `/p:AsApp=true` switch.

### 1. Project Configuration (`.csproj`)
Add conditional logic to your `<IsTestProject>` block:

```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <ImplicitUsings>enable</ImplicitUsings>
  <Nullable>enable</Nullable>
  
  <!-- If 'AsApp' is true, disable test project behavior to allow execution -->
  <IsTestProject Condition="'\$(AsApp)' == 'true'">false</IsTestProject>
  <IsTestProject Condition="'\$(AsApp)' != 'true'">true</IsTestProject>
  <IsPackable>false</IsPackable>
</PropertyGroup>
```

### 2. CLI Execution Commands

* **To run standard NUnit tests via IDE or runner:**
  ```bash
  dotnet test
  ```

* **To run a specific class and method as an application (Program.cs):**
  ```bash
  dotnet run /p:AsApp=true -- <ClassName> <MethodName> [Param1] [Param2]
  ```
  *Example:*
  ```bash
  dotnet run /p:AsApp=true -- CalculatorTests RunSum 10 25
  ```

---

## Alternative Setup: Programmatic Switch (`Program.cs`)

If you prefer avoiding MSBuild flags (`/p:AsApp=true`) entirely, you can permanently classify the project as a runnable application and use code routing instead.

### 1. Project Configuration (`.csproj`)
Set `<IsTestProject>` permanently to `false` and force the compiler to output an executable:

```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <ImplicitUsings>enable</ImplicitUsings>
  <Nullable>enable</Nullable>
  
  <!-- Always false so the project entry point (Program.cs) is never bypassed -->
  <IsTestProject>false</IsTestProject> 
  <OutputType>Exe</OutputType>
</PropertyGroup>

<ItemGroup>
  <!-- NUnitLite package is required to programmatically run tests via code -->
  <PackageReference Include="NUnitLite" Version="4.2.2" />
</ItemGroup>
```

### 2. Implementation (`Program.cs`)
Handle the switch routing programmatically using NUnitLite:

```csharp
using System;
using NUnitLite;

class Program
{
    static int Main(string[] args)
    {
        // Detect if the user passed the explicit test flag
        bool runAsTests = args.Length > 0 && args[0] == "--tests";

        if (runAsTests)
        {
            Console.WriteLine("Running as NUnit Test Suite...");
            return new AutoRun().Execute(args); 
        }

        // Otherwise, execute regular application reflection logic
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: dotnet run -- <ClassName> <MethodName> [Params...]");
            return 1;
        }

        // Call your Reflection handler here
        return 0;
    }
}
```

### 3. CLI Execution Commands

* **To run tests dynamically:**
  ```bash
  dotnet run -- --tests
  ```

* **To run your class/method logic:**
  ```bash
  dotnet run -- <ClassName> <MethodName> [Param1] [Param2]
  ```


