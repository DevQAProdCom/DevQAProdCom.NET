# 🤖 Code Name: AIMS (Agentic Interactions Management System) — Multi-Purpose/Multi-Functional .NET

The implementation/functionality of the backend part related to AI can be:
  - Used as a code library for `Backend` <-> `AI` <-> `Frontend` integrations.
  - Used as a console app.
  - Used as a `Frontend` foundation for the `Agentic Interactions Management System`, which is in development.

---

## Table of Contents

- [1. 🎯 GENERAL IDEA/ OVERALL AIMS](#1-general-idea-overall-aims)
  - [1.1 ⁉️ About](#11-about)
  - [1.2 🧭🔀 Three Supported Usage Approaches](#12-three-supported-usage-approaches)
    - [🧩🖥️ 1.2.1 CLI (File-Based) vs SDK (Code-Based) Comparison](#121-cli-file-based-vs-sdk-code-based-comparison)
    - [📄⚙️💻 1.2.2 High-Level Configuration Model](#122-high-level-configuration-model)

- [2. ☁️ HIGH LEVEL: IMPLEMENTATION/CONFIGURATION CONCEPTS](#2-high-level-implementation-configuration-concepts)
  - [2.1 🏗️ CLI-SDK Unified Architecture](#21-cli-sdk-unified-architecture)
  - [2.2 🕹️ Usage Modes: `Empty` (Isolated) vs `CopilotCli`](#22-usage-modes-empty-isolated-vs-copilotcli)
  - [2.3 Agents Configuration Options: YAML or JSON Formats](#23-agents-configuration-options-yaml-or-json-formats)
  - [2.4 ⚙️🔀📄 Fully CLI/File-Based Approach](#24-fully-cli-file-based-approach)
  - [2.5 ⚙️🔀💻 Fully SDK/Code-Based Approach](#25-fully-sdk-code-based-approach)
  - [2.6 ⚙️🔀📄💻 Hybrid File-Based + Code-Based Approach](#26-hybrid-file-based-code-based-approach)
  - [2.7 📐 Interfaces/Classes Layers](#27-interfaces-classes-layers)
    - [2.7.1 🚀🔗🔬 Diagram: High/Middle/Low Level Interfaces and Classes](#271-diagram-highmiddlelow-level-interfaces-and-classes)
    - [2.7.2 🚀 High-Level Interfaces](#272-high-level-interfaces)
    - [2.7.3 🔗 Middle-Level Interfaces and Classes](#273-middle-level-interfaces-and-classes)
    - [2.7.4 🔬 Low-Level Interfaces and Classes](#274-low-level-interfaces-and-classes)
    - [2.7.5 Microsoft Agent Framework: Agent Services Support](#275-microsoft-agent-framework-agent-services-support)
    - [2.7.6 Microsoft Agent Framework: Model Providers Comparison Support](#276-microsoft-agent-framework-model-providers-comparison-support)
    - [2.7.7 SDK and Endpoint Selection](#277-sdk-and-endpoint-selection)

- [3. 🔩 MIDDLE/LOW LEVEL: IMPLEMENTATION / CONFIGURATION CONCEPTS](#3-middle-low-level-implementation-configuration-concepts)
  - [3.1 ⚙️🧱 BASE CONFIGURATION](#31-base-configuration)
    - [3.1.1 Initial Setup & Usage Examples](#311-initial-setup--usage-examples)
    - [3.1.2 Authentication](#312-authentication)
    - [3.1.3 Copilot CLI Installation](#313-copilot-cli-installation)
    - [3.1.4 How to Run](#314-how-to-run)
      - [3.1.4.1 Run as NUnit Test (Class Method)](#3141-run-as-nunit-test-class-method)
      - [3.1.4.2 Run as Console App](#3142-run-as-console-app)
  - [3.2 🛠️🔧 DETAILED CONFIGURATION](#32-detailed-configuration)
    - [3.2.1 🛠️🕹️ Setup Mode: `CopilotCli` or `Empty`](#321-setup-mode-copilotcli-or-empty)
    - [3.2.2 🛠️🗂️ Setup Configuration Directories](#322-setup-configuration-directories)
      - [3.2.2.1 `Base Directory`](#3221-base-directory)
      - [3.2.2.2 `Working Directory`](#3222-working-directory)
      - [3.2.2.3 `Directory For Interaction Configuration Data`](#3223-directory-for-interaction-configuration-data)
      - [3.2.2.4 CLI File-Based Isolation using `Directory For Interaction Configuration Data`](#3224-cli-file-based-isolation-using-directory-for-interaction-configuration-data)
    - [3.2.3 🛠️🤖 Configure Agents](#323-configure-agents)
      - [3.2.3.1 File-Based and SDK-Based Agents Examples](#3231-file-based-and-sdk-based-agents-examples)
      - [3.2.3.2 Collections, Searchers and Locations Providers](#3232-collections-searchers-and-locations-providers)
      - [3.2.3.3 Session Configuration Builder](#3233-session-configuration-builder)
      - [3.2.3.4 Configuration Options](#3234-configuration-options)
        - [3.2.3.4.1 YAML File-Based](#32341-yaml-file-based)
        - [3.2.3.4.2 JSON File-Based](#32342-json-file-based)
        - [3.2.3.4.3 SDK-Based](#32343-sdk-based)
    - [3.2.4 🛠️📋 Configure Instructions / Rules](#324-configure-instructions--rules)
      - [3.2.4.1 File-Based and SDK-Based Instructions Examples](#3241-file-based-and-sdk-based-instructions-examples)
      - [3.2.4.2 Collections, Searchers and Locations Providers](#3242-collections-searchers-and-locations-providers)
      - [3.2.4.3 Session Configuration Builder](#3243-session-configuration-builder)
      - [3.2.4.4 Configuration Options](#3244-configuration-options)
        - [3.2.4.4.1 YAML File-Based](#32441-yaml-file-based)
        - [3.2.4.4.2 JSON File-Based](#32442-json-file-based)
        - [3.2.4.4.3 SDK-Based](#32443-sdk-based)
    - [3.2.5 🛠️🎓 Configure Skills](#325-configure-skills)
      - [3.2.5.1 File-Based and SDK-Based Skills Examples](#3251-file-based-and-sdk-based-skills-examples)
      - [3.2.5.2 Collections, Searchers and Locations Providers](#3252-collections-searchers-and-locations-providers)
      - [3.2.5.3 Session Configuration Builder](#3253-session-configuration-builder)
      - [3.2.5.4 Configuration Options](#3254-configuration-options)
        - [3.2.5.4.1 YAML File-Based](#32541-yaml-file-based)
        - [3.2.5.4.2 JSON File-Based](#32542-json-file-based)
        - [3.2.5.4.3 SDK-Based](#32543-sdk-based)
    - [3.2.6 🛠️🪝 Configure Hooks](#326-configure-hooks)
      - [3.2.6.1 File-Based and SDK-Based Hooks Examples](#3261-file-based-and-sdk-based-hooks-examples)
      - [3.2.6.2 Collections, Searchers and Locations Providers](#3262-collections-searchers-and-locations-providers)
      - [3.2.6.3 Session Configuration Builder](#3263-session-configuration-builder)
      - [3.2.6.4 Configuration Options](#3264-configuration-options)
        - [3.2.6.4.1 YAML File-Based](#32641-yaml-file-based)
        - [3.2.6.4.2 JSON File-Based](#32642-json-file-based)
        - [3.2.6.4.3 SDK-Based](#32643-sdk-based)
    - [3.2.7 🛠️🔌 Configure MCP Servers](#327-configure-mcp-servers)
      - [3.2.7.1 File-Based and SDK-Based MCP Servers Examples](#3271-file-based-and-sdk-based-mcp-servers-examples)
      - [3.2.7.2 Collections, Searchers and Locations Providers](#3272-collections-searchers-and-locations-providers)
      - [3.2.7.3 Session Configuration Builder](#3273-session-configuration-builder)
      - [3.2.7.4 Configuration Options](#3274-configuration-options)
        - [3.2.7.4.1 YAML File-Based](#32741-yaml-file-based)
        - [3.2.7.4.2 JSON File-Based](#32742-json-file-based)
        - [3.2.7.4.3 SDK-Based](#32743-sdk-based)
    - [3.2.8 🛠️🚦 Configure Permissions](#328-configure-permissions)
      - [3.2.8.1 Decision Contract and Evaluation](#3281-decision-contract-and-evaluation)
      - [3.2.8.2 Built-in Decisions](#3282-built-in-decisions)
      - [3.2.8.3 Configuration Options](#3283-configuration-options)
        - [3.2.8.3.1 YAML File-Based](#32831-yaml-file-based)
        - [3.2.8.3.2 JSON File-Based](#32832-json-file-based)
        - [3.2.8.3.3 SDK-Based](#32833-sdk-based)
      - [3.2.8.4 Collections and Injection](#3284-collections-and-injection)
      - [3.2.8.5 Build Flow and Ordering](#3285-build-flow-and-ordering)
    - [3.2.9 🛠️✅ Configure Agent Response Validators](#329-configure-agent-response-validators)
    - [3.2.10 🛠️🔍 Configure Logging and Monitoring](#3210-configure-logging-and-monitoring)
    - [3.2.11 ⚡ Invoke Agents](#3211-invoke-agents)
      - [3.2.11.1 Test Explorer vs Console App](#32111-test-explorer-vs-console-app)
      - [3.2.11.2 GitHub Copilot → MAF (Microsoft Agent Framework) `AIAgent`](#32112-github-copilot-maf-microsoft-agent-framework-aiagent)
      - [3.2.11.3 MAF (Microsoft Agent Framework) Invocation Flow](#32113-maf-microsoft-agent-framework-invocation-flow)

- [📎 LINKS](#-links)

## 1. 🎯 GENERAL IDEA/ OVERALL AIMS

### 1.1 ⁉️ About

This is an introductory **multi-purpose/multi-functional .NET Agentic Interactions Management System foundation** facade that makes one codebase cover almost every AI agent usage pattern.
`Usage Areas`: use it for **DEV AI INTEGRATIONS** and **AQA AI INTEGRATIONS** of any kind.

```text
┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
                                                                                        🚀                                                                                         
                                                                                    HIGH LEVEL                                                                                     
                                                                          Your Application/Usage Scenario                                                                          
                                                                                                                                                                                    
          ┌───────────────────────────────────┐                       ┌───────────────────────────────────┐                       ┌───────────────────────────────────┐           
                             🖥️                                                       💻                                                          🔀                            
                     𝗖𝗟𝗜/File-Based                                              𝗦𝗗𝗞/Code-Based                                        Hybrid File-Based + Code-Based             
                 Agents Config/Management                                    Agents Config/Management                                    Agents Config/Management                  
           ─────────────────┬─────────────────┘                       └─────────────────┬─────────────────┘                       └─────────────────┬─────────────────┘           
                            │                                                           │                                                           │                             
                            └───────────────────────────────────────────────────────────┼───────────────────────────────────────────────────────────┘                             
                                                                                         ▼                                                                                        
           ┌───────────────────────────────────┐                       ┌───────────────────────────────────┐                       ┌───────────────────────────────────┐           
                            🔧                                                         🎛️                                                         🤝                             
                       Single Agents                                               Orchestrators                                                Sub-Agents                         
                        Management                                                  Management                                                  Management                         
           └─────────────────┬─────────────────┘                       └─────────────────┬─────────────────┘                       └─────────────────┬─────────────────┘          
                             │                                                           │                                                           │                             
                             └───────────────────────────────────────────────────────────┼───────────────────────────────────────────────────────────┘                            
                                                                                         ▼                                                                                         
                                                                    ┌────────────────────┴────────────────────┐                                                                    
                                                                    ▼                                         ▼                                                                    
      ┌─────────────────────────────────────┐    ┌─────────────────────────────────────┐   ┌─────────────────────────────────────┐                                                 
                        🔐                                        🛡️                                        🌐                                                                  
              Fine-Grained Permission        ◄───│       Fine-Grained Configured                     𝗦𝗵𝗮𝗿𝗲𝗱 Environments                                                          
                Security Management                   Secured 𝗜𝘀𝗼𝗹𝗮𝘁𝗲𝗱 Environments                       Management                                                               
      └─────────────────────────────────────┘    └──────────────────┬──────────────────┘   └──────────────────┬──────────────────┘                                                 
                                                                    │                                         │                                                                    
                                                                    └────────────────────┬────────────────────┘                                                                    
                                                                                         ▼                                                                                         
                             ┌───────────────────────────────────────────────────────────┼───────────────────────────────────────────────────────────┐                             
                             ▼                                                           ▼                                                           ▼                             
           ┌───────────────────────────────────┐                       ┌───────────────────────────────────┐                       ┌───────────────────────────────────┐           
                            ✅                                                         📊                                                          📝                             
                      Agent Responses                                         Token Consumption/Costs                                             Logging                          
                   Validation Management                                    Monitoring and Management                                            Management                         
           └───────────────────────────────────┘                       └───────────────────────────────────┘                       └───────────────────────────────────┘           
├─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┤
                                                                                        🧩                                                                                         
                                                                                   MIDDLE LEVEL                                                                                    
                                                                             Microsoft Agent Framework                                                                             
                                                                                                                                                                                   
                                                            Orchestration • Retry • Streaming • Handlers • Abstraction                                                             
                                                                                                                                                                                   
                                                                      ┌─────────────────────────────────────┐                                                                      
                                                                                        📦                                                                                         
                                                                              Multiple Agent Services                                                                              
                                                                                Support/Management                                                                                 
                                                                      └──────────────────┬──────────────────┘                                                                      
                                                                                         │                                                                                         
                               ┌────────────────────────────┬────────────────────────────┼────────────────────────────┬────────────────────────────┐                               
                               ▼                            ▼                            ▼                            ▼                            ▼                               
                  ┌─────────────────────────┐  ┌─────────────────────────┐  ┌─────────────────────────┐  ┌─────────────────────────┐  ┌─────────────────────────┐                  
                  │    Microsoft Foundry    │  │     GitHub Copilot      │  │     Copilot Studio      │  │    Anthropic Claude     │  │           A2A           │                  
                  │                         │  │                         │  │                         │  │ (currently Python only) │  │                         │                  
                  └─────────────────────────┘  └─────────────────────────┘  └─────────────────────────┘  └─────────────────────────┘  └─────────────────────────┘                  
                                                                                                                                                                                   
                                                                      ┌─────────────────────────────────────┐                                                                      
                                                                                        🔌                                                                                         
                                                                               Multi-Model-Providers                                                                               
                                                                                Support/Management                                                                                 
                                                                      └──────────────────┬──────────────────┘                                                                      
                                                                                         │                                                                                         
                             ┌───────────────────────┬───────────────────────┬───────────┴───────────┬───────────────────────┬───────────────────────┐                             
                             ▼                       ▼                       ▼                       ▼                       ▼                       ▼                             
                  ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐                  
                  │    Azure OpenAI     │ │       OpenAI        │ │  Microsoft Foundry  │ │      Anthropic      │ │       Ollama        │ │    Foundry Local    │                  
                  └─────────────────────┘ └─────────────────────┘ └─────────────────────┘ └─────────────────────┘ └─────────────────────┘ └─────────────────────┘                  
                              ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐ ┌─────────────────────┐                              
                              │   Amazon Bedrock    │ │    Google Gemini    │ │        ONNX         │ │        Dapr         │ │       Mistral       │                              
                              └─────────────────────┘ └─────────────────────┘ └─────────────────────┘ └─────────────────────┘ └─────────────────────┘                              
├─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┤
                                                                                        🔌                                                                                         
                                                                                     LOW LEVEL                                                                                     
                                                                                 Provider Runtime                                                                                  
                                                                                                                                                                                   
                                   ┌───────────────────────────────────┐                                   ┌───────────────────────────────────┐                                   
                                                    🐙                                                                     🧩                                                     
                                            GitHub Copilot SDK                                                        Other Providers                                              
                                            (current provider)                                                            Support                                                  
                                   └─────────────────┬─────────────────┘                                   └───────────────────────────────────┘                                   
                                                     │                                                                                                                             
                                                     ▼                                                                                                                             
                                   ┌───────────────────────────────────┐                                                                                                           
                                                     🖥️                                                                                                                            
                                           Copilot CLI (bundled)                                                                                                                   
                                             JSON-RPC runtime                                                                                                                      
                                   └───────────────────────────────────┘                                                                                                           
└─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘
```
> [📘 Microsoft Agent Framework](https://github.com/microsoft/agent-framework) — open, multi-language framework for production-grade AI agents and multi-agent workflows in .NET and Python.
>
> [📘 Microsoft Agent Services](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/agent-services/) — compare managed and remote agent services.
>
> [📘 Microsoft Model Providers](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/?pivots=programming-language-csharp) — compare model inference clients by language.
>
> [📘 GitHub Copilot SDK](https://github.com/github/copilot-sdk) — SDKs that talk to the Copilot CLI server via JSON-RPC.

---

### 1.2 🧭🔀 Three Supported Usage Approaches

| # | Approach | Best for |
|---|----------|----------|
| 1 | **Fully CLI-Based/File-Based** | Use `.github/agents`, `.github/instructions`, `.github/skills`, `.github/hooks`, `mcp.json` files and run in UI Applications or CLI, or with AIMS using `CopilotClientMode.CopilotCli`. |
| 2 | **Fully SDK-Based/Code-Based** | Build everything in C# with `CopilotClientMode.Empty` for full security isolation and control or using `CopilotClientMode.CopilotCli` for CLI support mode. |
| 3 | **Hybrid File-Based + Code-Based** | Describe agents in YAML/JSON inside `.agent.md` files, add SDK-only properties when needed, then run by name and/or configuration. |

#### 🧩🖥️ 1.2.1 CLI (File-Based) vs SDK (Code-Based) Comparison

```text
┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
                         🧩                                       🧩⚙️🖥️                                     🖥️
                         CLI                                       Hybrid                                      SDK
                    (File-Based)                           File-Based + Code-Based                        (Code-Based) 
                     Approach                                     Approach                                  Approach              
                                                          Combines Both Advantages                                                                                             
└─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘

                                                                    🎯
Quick, file-driven setup                                    ← Usage Purpose →           FULL-STACK Backend + AI + Frontend Integration

┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
                                                           ⚙️ Configuration Sources                                                                         
└─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘
                                                                    📁
Usually files in `.{provider-directory}/agents`,          ← Locations Management →      Any local/remote location; configured or SDK-integrated from libraries
`{provider-directory}/rules`,`{provider-directory}/skills` etc.
                                                                    🪝
File-Based `.{provider-directory}/hooks`                         ← Hooks →              File-Based + SDK/Code-Based
                                                                    🔌
File-Based `mcp.json`                                         ← MCP Servers →           File-Based + SDK/Code-Based
                                                                    🗂️
Limited                                                  ← Multi-Config per Agent →     Reusable multiple configurations per agent in YAML and JSON formats

┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
                                                              🔒 Security                                                                                                
└─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘
                                                                   🛡️
Shared CLI ambient context                                    ← Isolation →             Per-session isolated environment
                                                                   🚦
CLI defaults                                                 ← Permissions →            Fine-grained, code-based permission decisions
                                                                   🔐
Hooks or environment variables                          ← Credentials Management →      Flexible runtime secret retrieval and management in code

┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
                                                              📜 Scripting                                                                                                
└─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘
                                                                    💻
Requires add-in scripts/code for hooks                          ← Coding →              Native code extensibility for hooks, MCP tools, RAG, and validators
                                                                    ✅
Hook scripts                                             ← Response Validation →        Built-in validator libraries + automated retry logic with detailed error messages

┌─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐
                                                         🔍 Logging and Debugging                                                                                         
└─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘
                                                                    📝
CLI-level                                                      ← Logging →              Fine-grained per-request/response/event logging using any Logging Library
                                                                    🐞
Either absent or very difficult, especially for hooks          ← Debugging →             Full-stack debugging across all operations
```

#### 📄⚙️💻 1.2.2 High-Level Configuration Model

```text
             📄 Agent file (.agent.md)
As a prompt source with or without configuration in YAML format  ───────────────────────────────────────────────────────────┐
                                                                                                                             ├──►  🔧 SessionConfigBuilder  ──►  🤖 AIAgent (MAF)  ──►  streaming + validation
             ⚙️ JSON config (.configuration.json)                                                                           │
As a substitutable configuration in JSON format; may or may not reference a prompt in an .agent.md file by agent name  ─────┘                   ↑
                                                                                                                                                │
💻 SDK code (agents, instructions/rules, skills, hooks, MCP servers, permissions, validators, handlers, libraries/infrastructure)  ────────────┘
```

- **Files** supply prompts and declarative config.
- **Code** supplies isolation, permissions, validation, retry, logging, and orchestration.
- **MAF** (Microsoft Agent Framework) makes the provider replaceable.
- **GitHub Copilot SDK** is the first provider.

---

## 2. ☁️ HIGH LEVEL: IMPLEMENTATION/CONFIGURATION CONCEPTS

### 2.1 🏗️ CLI-SDK Unified Architecture

For the `GitHub Copilot` Provider, the CLI/SDK unified architecture is described below:

 ```text
      Your Application
           ↓
        SDK Client    ← .NET / Python / Node SDK
         ↓ JSON-RPC
 Copilot CLI (server mode)
```
---

### 2.2 🕹️ Usage Modes: `Empty` (Isolated) vs `CopilotCli`

For `GitHub Copilot` Provider integration, 2 modes are supported:

```csharp
public enum CopilotClientMode
{
    Empty,        // 🛡️ full isolation — no tools exposed by default
    CopilotCli    // 🖥️ shares ambient CLI context
}
```

| Mode | When to use |
|------|-------------|
| `Empty` | Multi-user servers, tests, AQA, production. Requires `BaseDirectory`/`SessionFs` and explicit `AvailableTools`. Default. |
| `CopilotCli` | Local coding agent sharing context with GitHub Copilot CLI. Not for multi-user servers. |

In `Empty` mode the builder automatically injects `BuiltInTools.Isolated`:

```text
builtin:ask_user, builtin:task_complete, builtin:exit_plan_mode, builtin:task,
builtin:read_agent, builtin:write_agent, builtin:list_agents, builtin:send_inbox,
builtin:context_board, builtin:skill
```

---

### 2.3 Agents Configuration Options: YAML or JSON Formats

Support for different configurations can be useful for testing/executing the same agent prompt under various conditions, such as behavior and results with different models,
different MCP server tools, or different instructions/skills/hooks.

An agent **must** have a name and a prompt. But all other configuration properties can be kept in two format options:

1. **`.agent.md` file** — YAML front-matter + Markdown body (required minimum: `name` + prompt).
2. **`.configuration.json` file** — JSON configuration that references agent or extends an agent configuration.

Both can coexist. The `.agent.md` supplies the prompt + YAML configuration; the `.configuration.json` can override tools, permissions, instructions, skills, hooks, MCP servers, and subagents.

Example Configuration in YAML format inside `.agent.md` file:

```markdown
---

name: agent-identifier                                                        # 🧩 provider CLI supported YAML property
description: agent-description                                                # 🧩 provider CLI supported YAML property
tools: [view, create]                                                         # 🧩 provider CLI supported YAML property
model: claude-sonnet-5.5                                                      # 🧩 provider CLI supported YAML property
custom-metadata:                                                              # 💻 SDK supported YAML property
  configurations:
    - configurationIdentifier: CONFIGURATION-IDENTIFIER-1                     # 💻 SDK supported YAML property
      permissions: ["permission-identifier-built-in-tools"]                   # 💻 SDK supported YAML property
      instructions: ["instruction-identifier"]                                # 💻 SDK supported YAML property
      skills: ["skill-identifier"]                                            # 💻 SDK supported YAML property
      hooks: ["hook-identifier"]                                              # 💻 SDK supported YAML property
      mcpServers: ["playwright-mcp"]                                          # 💻 SDK supported YAML property
      subagents: ["subagent-identifier"]                                      # 💻 SDK supported YAML property
```

Example Configuration in JSON format in distinct `.configuration.json` file:

```json
{
  "name": "agent-identifier",                                 # 💻 SDK supported JSON property mapped to 🧩 provider CLI supported property
  "description": "agent-description",                         # 💻 SDK supported JSON property mapped to 🧩 provider CLI supported property
  "configurationIdentifier": "CONFIGURATION-IDENTIFIER-2",    # 💻 SDK supported JSON property
  "tools": [  "mcp-server-identifier/*"],                     # 💻 SDK supported JSON property mapped to 🧩 provider CLI supported property  
  "model": "claude-opus-5.5",                                 # 💻 SDK supported JSON property mapped to 🧩 provider CLI supported property 
  "permissions": [ "mcp-server-identifier-allow-all"],        # 💻 SDK supported JSON property
  "instructions": [ "instruction-identifier"],                # 💻 SDK supported JSON property
  "skills": [    "skill-identifier"],                         # 💻 SDK supported JSON property
  "hooks": [ "instruction-identifier"],                       # 💻 SDK supported JSON property
  "mcpServers": ["mcp-server-identifier"],                    # 💻 SDK supported JSON property
  "subagents": ["subagent-identifier"]                        # 💻 SDK supported JSON property
}
```

---


### 2.4 ⚙️🔀📄 Fully CLI/File-Based Approach

This is the usual approach by which CLI environments discover and apply agent configurations.
It is supplied out of the box.

AIMS (Agentic Interactions Management System) supports `GitHub Copilot` Provider integration by setting mode `CopilotClientMode.CopilotCli` and using a working directory that contains a `.github` directory.

```markdown
---
name: agent-identifier                          # 🧩 provider CLI supported YAML property
description: agent-description                  # 🧩 provider CLI supported YAML property
tools:                                          # 🧩 provider CLI supported YAML property
  - view
  - create
model: claude-haiku-4.5                         # 🧩 provider CLI supported YAML property
---

# Prompt body                                   
You are {some_type_of} Agent...
```


```text
{RepoRoot}/.{provider-directory}/             # 🧩 provider CLI supported directory (for instance, `.github`)
├── agents/                                   # 🧩 provider CLI supported directory
│   └── agent-identifier.agent.md
├── instructions/                             # 🧩 provider CLI supported directory (rules/instructions) 
│   └── instruction-identifier.instructions.md
├── skills/                                   # 🧩 provider CLI supported directory
│   └── my-skill/
│       └── SKILL.md
├── hooks/                                    # 🧩 provider CLI supported directory
│   └── hook-identifier.hook.json
├── mcp.json                                  # 🧩 provider CLI supported file or files with a specific extension
└── global-instructions.md                    # 🧩 provider CLI supported file (for instance, `copilot-instructions.md`)
```

### 2.5 ⚙️🔀💻 Fully SDK/Code-Based Approach

Everything is configured in C# with `SessionConfigBuilder` and `CopilotClientOptionsBuilder`.
This example uses GitHub Copilot, but other providers can be wired into the same MAF layer.
AIMS (Agentic Interactions Management System) supports `GitHub Copilot` Provider integration by setting mode `CopilotClientMode.Empty`, which provides the maximum level of isolation/security but requires manual configuration.
Alternatively, use `CopilotClientMode.CopilotCli` with a working directory that contains a `.github` directory.

```csharp
await using var agent = factory
    .GetGitHubCopilotAiAgentInteractor()

    // 🔧 Client options
    .WithCopilotClientOptions(builder => builder
        .WithMode(CopilotClientMode.Empty)
        .WithBaseDirectory()                    // defaults to temp dir
        .WithWorkingDirectory("<your-work-dir>")
        .WithGitHubToken("<token>")
        .WithUseLoggedInUser(false)
        .WithLogLevel(CopilotLogLevel.Debug)
        .WithSessionIdleTimeoutSeconds(300)
        .WithEnableRemoteSessions(false)
    )

    // 🎯 Session config
    .WithSessionConfig(builder => builder
        // Agents
        .WithPrimaryAgent("agent-identifier")
        .WithAgent("sub-agent-identifier")
        .WithAgentFromFile("sub-agent-file-path")
        .WithAgentsFromDirectory("sub-agents-directory-path")

        // Instructions / Rules
        .WithInstruction("instruction-identifier")
        .WithInstructionFromFile("<instruction-file-path>")
        .WithInstructionsFromDirectory("<instructions-dir>")

        // Skills
        .WithSkill("skill-identifier")
        .WithSkillFromFile("<skill-file-path>")
        .WithSkillDirectories("<skills-dir>")

        // Hooks
        .WithEnableFileHooks(true)
        .WithFileBasedHook("File-Based-hook-identifier")
        .WithFileBasedHooksFromFile("<File-Based-hook-file-path>")
        .WithSdkBasedHooks(h => h
            .WithOnSessionStart(async (s, e) => { /* ... */ }))

        // MCP servers
        .WithFileBasedMcpServer("File-Based-mcp-server-identifier")
        .WithFileBasedMcpServersFromFile("<File-Based-mcp-server-file-path>")
        .WithSdkBasedMcpServer("SDK-Based-mcp-server-identifier")

        // Permissions
        .WithPermission("permission-identifier")

        // Session tuning
        .WithModel("claude-haiku-4.5")
        .WithWorkingDirectory("<your-work-dir>")
        .WithStreaming(true)
        .WithSelectiveIsolation()
    )

    .WithPrompt("Execute agent with parameters...")
    .WithMaxAttempts(3)
    .WithResponseValidator(new AgentResponseValidator());

await agent.InvokeAiAgentWithStreamingAsync();
```

### 2.6 ⚙️🔀📄💻 Hybrid File-Based + Code-Based Approach

Keep the agent prompt and provider configuration in the `agent-identifier.agent.md` file, add SDK-only properties in `custom-metadata`, and run it from code by name or configuration.

AIMS (Agentic Interactions Management System) supports `GitHub Copilot` Provider integration by setting mode `CopilotClientMode.Empty`, which provides the maximum level of isolation/security but requires manual configuration.
Alternatively, use `CopilotClientMode.CopilotCli` with a working directory that contains a `.github` directory.

```markdown
---

name: agent-identifier                                                          # 🧩 provider CLI supported YAML property
description: agent-description                                                  # 🧩 provider CLI supported YAML property
tools: [view, create]                                                           # 🧩 provider CLI supported YAML property
model: claude-sonnet-5.5                                                        # 🧩 provider CLI supported YAML property
custom-metadata:                                                                # 💻 SDK supported YAML property
  configurations:
    - configurationIdentifier: CONFIGURATION-IDENTIFIER-1                       # 💻 SDK supported YAML property
      permissions: ["permission-identifier-built-in-tools"]                     # 💻 SDK supported YAML property
      instructions: ["instruction-identifier"]                                  # 💻 SDK supported YAML property
      mcpServers: ["playwright-mcp"]                                            # 💻 SDK supported YAML property
      hooks: ["hook-identifier"]                                                # 💻 SDK supported YAML property
      subagents: ["sub-agent-1"]                                                # 💻 SDK supported YAML property
  - configurationIdentifier: CONFIGURATION-IDENTIFIER-2                         # 💻 SDK supported YAML property
      model: claude-opus-5.5                                                    # 🧩 provider CLI supported YAML property
      tools: ["mcp-server-identifier/view", "mcp-server-identifier/create"]     # 🧩 provider CLI supported YAML property
      permissions: ["permission-identifier-mcp-server-tools"]                   # 💻 SDK supported YAML property
      instructions: ["instruction-identifier"]                                  # 💻 SDK supported YAML property
      mcpServers: ["mcp-server-identifier"]                                     # 💻 SDK supported YAML property
      hooks: ["hook-identifier"]                                                # 💻 SDK supported YAML property
      subagents: ["sub-agent-1"]                                                # 💻 SDK supported YAML property

---

You are {some_type_of} Agent...
```

Or use the `JSON` configuration from the `agents-configurations` directory.

**💻 SDK supported Configuration Option**

```json
{
  "name": "agent-identifier",                                 # 💻 SDK supported JSON property mapped to 🧩 provider CLI supported property
  "description": "agent-description",                         # 💻 SDK supported JSON property mapped to 🧩 provider CLI supported property
  "configurationIdentifier": "CONFIGURATION-IDENTIFIER-2",    # 💻 SDK supported JSON property
  "tools": [                                                  # 💻 SDK supported JSON property mapped to 🧩 provider CLI supported property
    "mcp-server-identifier/view",
    "mcp-server-identifier/create"
  ],
  "permissions": [                                            # 💻 SDK supported JSON property
    "mcp-server-identifier/view", 
    "mcp-server-identifier/create"
  ],
  "instructions": [                                           # 💻 SDK supported JSON property
    "instruction-identifier"
  ],
  "mcpServers": ["mcp-server-identifier"]                     # 💻 SDK supported JSON property
}
```

```text
{RepoRoot}/.{provider-directory}/                     # 🧩 provider CLI supported directory (for instance, `.github`) + 💻 SDK supported directory
├── agents/                                           # 🧩 provider CLI supported directory + 💻 SDK supported directory
│   └── agent-identifier.agent.md
├── agents-configurations/                            # 💻 SDK supported directory
│   └── CONFIGURATION-IDENTIFIER-2.configuration.json
├── instructions/                                     # 🧩 provider CLI supported directory (rules/instructions) + 💻 SDK supported directory
│   └── instruction-identifier.instructions.md
├── skills/                                           # 🧩 provider CLI supported directory + 💻 SDK supported directory
│   └── my-skill/
│       └── SKILL.md
├── hooks/                                            # 🧩 provider CLI supported directory + 💻 SDK supported directory
│   └── hook-identifier.hook.json
├── mcp-servers/                                      # 💻 SDK supported directory
│   └── mcp-server-identifier.mcp.json
├── mcp.json                                          # 🧩 provider CLI supported file or files with specific extension + 💻 SDK supported directory
└── global-instructions.md                            # 🧩 provider CLI supported file (for instance, `copilot-instructions.md`) + 💻 SDK supported directory
```

Run it:

```csharp
await using var agent = factory
    .GetGitHubCopilotAiAgentInteractor()
    .WithPrimaryAgent("agent-identifier", "CONFIGURATION-IDENTIFIER-1")
  //.WithPrimaryAgent("agent-identifier", "CONFIGURATION-IDENTIFIER-2")
    .WithPrompt("Execute agent with parameters...");

await agent.InvokeAiAgentWithStreamingAsync();
```
---

### 2.7 📐 Interfaces/Classes Layers

#### 2.7.1 🚀🔗🔬 Diagram: High/Middle/Low Level Interfaces and Classes

```text
┌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌┬╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌┬╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌┐
                   🚀 HIGH LEVEL                                          🔗 MIDDLE LEVEL                                     🔌 LOW LEVEL                                             
                Shared AI Abstractions                           Microsoft Agent Framework Orchestration                    GitHub Copilot Provider                   
                                                                                                                                                                     
  ┌─────────────────────────────────────────────────────┐      ┌────────────────────────────────────────┐      ┌──────────────────────────────────────────────────────┐  
   💬 Agent Interaction                                         🤖 Agent Interactors                           🤝 Copilot Client                                       
    IAiAgentInteractor                                           IMicrosoftAiAgentInteractorT<T>                 IGitHubCopilotClientService                             
    IAiInteractionRequest                                        IMicrosoftAiAgentInteractor                   └──────────────────────────────────────────────────────┘  
    IAiInteractionDataBank                                     ├╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌┤                                                             
  └─────────────────────────────────────────────────────┘        MicrosoftAiAgentInteractor [class]            ┌──────────────────────────────────────────────────────┐  
                                                               └────────────────────────────────────────┘       🐙 Provider Configuration and Extensions                
  ┌─────────────────────────────────────────────────────┐                                                        IGitHubCopilotAiAgentConfigurationWithCustomMetadata    
    ✅ Interaction Processing                                  ┌────────────────────────────────────────┐        IGitHubCopilotAiInstructionConfiguration                
    IAiInteractionResultValidator                                🧱 Framework and Provider Classes               IGitHubCopilotAiSkillConfiguration                      
    IAiInteractionHandler                                        GitHubCopilotAiAgentInteractor [class]          IGitHubCopilotSdkBasedSessionHook                       
  └─────────────────────────────────────────────────────┘          ↳ creates AIAgent [framework class]           IGitHubCopilotSdkBasedMcpServer                         
                                                                 AgentRunOptionsBuilder [class]                  IGitHubCopilotPermissionDecision                        
  ┌─────────────────────────────────────────────────────┐        AgentResponseUpdate / AIContent               └──────────────────────────────────────────────────────┘  
    ⚙️ Agent Configuration                                         [framework types]                                                                                  
    IAiAgentConfiguration                                       └────────────────────────────────────────┘     ┌──────────────────────────────────────────────────────────────────────────────┐  
    IAiAgentConfigurationWithCustomMetadata                      AgentResponseUpdate / AIContent stream         🏗️ Concrete Builders / SDK Classes                      
  └─────────────────────────────────────────────────────┘                          ▼                               SessionConfigBuilder [class]  → SessionConfig [SDK class]                         
                                                                ┌────────────────────────────────────────┐         CopilotClientOptionsBuilder [class]  → CopilotClientOptions [SDK class]                            
  ┌─────────────────────────────────────────────────────┐        🎧 Streaming Content Handling                 └──────────────────────────────────────────────────────────────────────────────┘
    🔎 Locations and Entity Search                               IAiContentHandler                                                  
    ILocationsProvider                                          └────────────────────────────────────────┘    
    ISearchEntitiesInLocations<T>                                                                                                                                  
  └─────────────────────────────────────────────────────┘                                                                                                          
  locations / discovered entities  │                                                                                                                               
                                   ▼                                                                                                                               
  ┌─────────────────────────────────────────────────────┐                                                                                                          
    📦 Entity Collections                                                                                                                                       
    IAiEntitiesCollection<TAiEntityConfiguration>                                                                                                                  
    IAiEntityWithTConfigurationType<TConfigurationType>                                                                                                          
  └─────────────────────────────────────────────────────┘                                                                                                          
└╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌┴╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌┴╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌╌┘
```

#### 2.7.2 🚀 High-Level Interfaces

| Interface/Class | Short responsibility | Project location |
|---|---|---|
| `IAiAgentInteractor` | Provides provider-neutral streaming invocation and asynchronous disposal. | `DevQAProdCom.NET.AI.Shared/Interfaces/Interactions/IAiAgentInteractor.cs` |
| `IAiInteractionRequest` | Carries the prompt used for an AI interaction. | `DevQAProdCom.NET.AI.Shared/Interfaces/Interactions/IAiInteractionRequest.cs` |
| `IAiInteractionDataBank` | Stores interaction output collected by streaming content handlers. | `DevQAProdCom.NET.AI.Shared/Interfaces/Interactions/IAiInteractionDataBank.cs` |
| `IAiInteractionResultValidator` | Validates the collected interaction result and returns a validation outcome. | `DevQAProdCom.NET.AI.Shared/Interfaces/Interactions/IAiInteractionResultValidator.cs` |
| `IAiInteractionHandler` | Handles string-based interaction events, records their data, and performs finalization. | `DevQAProdCom.NET.AI.Shared/Interfaces/Interactions/IAiInteractionHandler.cs` |
| `IAiAgentConfiguration` | Defines a provider-neutral agent configuration with identity, settings, custom data, and a configuration identifier. | `DevQAProdCom.NET.AI.Shared/Interfaces/Agents/IAiAgentConfiguration.cs` |
| `IAiAgentConfigurationWithCustomMetadata` | Defines provider-neutral agent configuration data with access to custom metadata and named configurations. | `DevQAProdCom.NET.AI.Shared/Interfaces/Agents/IAiAgentConfigurationWithCustomMetadata.cs` |
| `ILocationsProvider` | Provides locations used to discover AI entities. | `DevQAProdCom.NET.AI.Shared/Interfaces/ILocationsProvider.cs` |
| `ISearchEntitiesInLocations<T>` | Searches a specified location for entities, with optional extended search. | `DevQAProdCom.NET.AI.Shared/Interfaces/ISearchEntitiesInLocations.cs` |
| `IAiEntitiesCollection<TAiEntityConfiguration>` | Stores, loads, and retrieves configured AI entities by identifier or file path. | `DevQAProdCom.NET.AI.Shared/Interfaces/IAiEntitiesCollection.cs` |
| `IAiEntityWithTConfigurationType<TConfigurationType>` | Represents an AI entity with strongly typed configuration data. | `DevQAProdCom.NET.AI.Shared/Interfaces/IAiEntityWithTConfigurationType.cs` |

#### 2.7.3 🔗 Middle-Level Interfaces and Classes

| Interface/Class | Short responsibility | Project location |
|---|---|---|
| `IMicrosoftAiAgentInteractorT<T>` | Defines a fluent provider facade whose concrete facade type is determined by `T`; creates an `AIAgent` and delegates execution. | `DevQAProdCom.NET.AI.MicrosoftAgentFramework/Interfaces/IMicrosoftAiAgentInteractorT.cs` |
| `IMicrosoftAiAgentInteractor` | Defines the provider-agnostic core contract for prompts, run options, content handlers, validation, retries, and invocation. | `DevQAProdCom.NET.AI.MicrosoftAgentFramework/Interfaces/IMicrosoftAiAgentInteractor.cs` |
| `MicrosoftAiAgentInteractor` | Concrete class that orchestrates agent execution, streamed-content processing, response validation, and retry attempts. | `DevQAProdCom.NET.AI.MicrosoftAgentFramework/OperativeClasses/MicrosoftAiAgentInteractor.cs` |
| `IAiContentHandler` | Processes streamed `AIContent`, writes interaction data, and performs finalization. | `DevQAProdCom.NET.AI.MicrosoftAgentFramework/Interfaces/IAiContentHandler.cs` |
| `AIAgent` | Microsoft Agent Framework class used to create sessions and execute streaming agent interactions. | External: `Microsoft.Agents.AI` package |
| `GitHubCopilotAiAgentInteractor` | Builds the Copilot client and session configuration, creates the `AIAgent`, and manages provider resources. | `DevQAProdCom.NET.AI.MicrosoftAgentFramework/OperativeClasses/GitHubCopilotAiAgentInteractor.cs` |
| `AgentRunOptionsBuilder` | Fluent builder for configuring and creating agent run options. | `DevQAProdCom.NET.AI.MicrosoftAgentFramework/Builders/AgentRunOptionsBuilder.cs` |
| `AgentResponseUpdate` | Represents one update in a streaming agent response. | External: `Microsoft.Agents.AI` package |
| `AIContent` | Represents an individual content item carried by a streaming response update. | External: `Microsoft.Extensions.AI` package |

#### 2.7.4 🔬 Low-Level Interfaces and Classes

| Interface/Class | Short responsibility | Project location |
|---|---|---|
| `IGitHubCopilotClientService` | Configures, creates, retrieves, and disposes the GitHub Copilot client. | `DevQAProdCom.NET.AI.GitHubCopilot/Interfaces/IGitHubCopilotClientService.cs` |
| `IGitHubCopilotAiAgentConfigurationWithCustomMetadata` | Specializes provider-neutral agent configuration with custom metadata for GitHub Copilot. | `DevQAProdCom.NET.AI.GitHubCopilot/Interfaces/IGitHubCopilotAiAgentConfigurationWithCustomMetadata.cs` |
| `IGitHubCopilotAiInstructionConfiguration` | Defines GitHub Copilot instruction identity and optional path patterns to which the instruction applies. | `DevQAProdCom.NET.AI.GitHubCopilot/Interfaces/IGitHubCopilotAiInstructionConfiguration.cs` |
| `IGitHubCopilotAiSkillConfiguration` | Defines GitHub Copilot skill identity and the tools the skill may use. | `DevQAProdCom.NET.AI.GitHubCopilot/Interfaces/IGitHubCopilotAiSkillConfiguration.cs` |
| `IGitHubCopilotSdkBasedSessionHook` | Adds an identified SDK-Based hook to the Copilot session hooks. | `DevQAProdCom.NET.AI.GitHubCopilot/Interfaces/IGitHubCopilotSdkBasedSessionHook.cs` |
| `IGitHubCopilotSdkBasedMcpServer` | Adds an identified SDK-Based MCP server to the session configuration and serializes it to JSON. | `DevQAProdCom.NET.AI.GitHubCopilot/Interfaces/IGitHubCopilotSdkBasedMcpServer.cs` |
| `IGitHubCopilotPermissionDecision` | Provides the asynchronous function used to decide a Copilot permission request. | `DevQAProdCom.NET.AI.GitHubCopilot/Interfaces/IGitHubCopilotPermissionDecision.cs` |
| `SessionConfigBuilder` | Discovers and assembles agents, instructions, skills, tools, hooks, MCP servers, permissions, and other session settings. | `DevQAProdCom.NET.AI.GitHubCopilot/Builders/SessionConfigBuilder.cs` |
| `SessionConfig` | Contains the GitHub Copilot SDK session configuration produced by `SessionConfigBuilder`. | External: `GitHub.Copilot` namespace from `Microsoft.Agents.AI.GitHub.Copilot` package |
| `CopilotClientOptionsBuilder` | Fluent builder for Copilot client mode, directories, authentication, logging, connection, and other client options. | `DevQAProdCom.NET.AI.GitHubCopilot/Builders/CopilotClientOptionsBuilder.cs` |
| `CopilotClientOptions` | Contains the GitHub Copilot SDK client options produced by `CopilotClientOptionsBuilder`. | External: `GitHub.Copilot` namespace from `Microsoft.Agents.AI.GitHub.Copilot` package |


> Other providers can be added at the low layer without touching the high layer.

#### 2.7.5 Microsoft Agent Framework: Agent Services Support

Agent services provide a remote or managed agent runtime rather than only model inference. The service can own the agent definition, hosted tools, permissions, sessions, or execution lifecycle while Agent Framework exposes a consistent run interface to your application.

For inference clients where your application owns the agent definition and orchestration, see [Model providers](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/?pivots=programming-language-csharp).

| Agent Service | C# | Python | Go | What the service owns |
| --- | :-: | :-: | :-: | --- |
| [Microsoft Foundry](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/agent-services/foundry) | ✅ | ✅ | ✅ | Prompt or Hosted Agent definition, versions, hosted tools, conversations, and service-side execution |
| [GitHub Copilot](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/agent-services/github-copilot) | ✅ | ✅ | ✅ | Coding-agent runtime, sessions, permissions, built-in shell/file/URL capabilities, and MCP connections |
| [Copilot Studio](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/agent-services/copilot-studio) | ✅ | ✅ | ❌ | Published agent topics, knowledge, actions, plugins, and remote execution |
| [Anthropic Claude](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/agent-services/anthropic-claude) | ❌ | ✅ | ❌ | Claude Agent SDK runtime, sessions, permissions, built-in tools, and MCP connections |
| [A2A](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/agent-services/a2a) | ✅ | ✅ | ✅ | Remote A2A-compliant agent definition, tools, sessions, tasks, and execution |

#### 2.7.6 Microsoft Agent Framework: Model Providers Comparison Support

Model providers supply the inference client used by an Agent Framework agent. Your application owns the agent definition, instructions, tools, middleware, and session policy while the provider supplies model inference and provider-hosted capabilities.

For remote or managed runtimes that own an agent definition, permissions, or service-side execution, see [Agent Services](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/agent-services/). For custom framework agent implementations, use [Custom Agents](https://learn.microsoft.com/en-us/agent-framework/concepts/agents/custom-agents?pivots=programming-language-csharp).

| Provider | Function Tools | Structured Outputs | Code Interpreter | File Search | MCP Tools | Background Responses |
| --- | :-: | :-: | :-: | :-: | :-: | :-: |
| [Azure OpenAI](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/azure-openai) | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| [OpenAI](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/openai) | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| [Microsoft Foundry](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/microsoft-foundry) | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| [Anthropic](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/anthropic) | ✅ | ✅ | ✅ | ❌ | ✅ | ❌ |
| [Ollama](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/ollama) | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |
| [Foundry Local](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/foundry-local) | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ |
| [Amazon Bedrock](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/amazon-bedrock) | ✅ | Varies | ❌ | ❌ | ❌ | ❌ |
| [Google Gemini](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/google-gemini) | ✅ | ✅ | ✅ | ✅ | ✅ | ❌ |
| [ONNX](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/onnx) | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| [Dapr](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/dapr) | Varies | Varies | Varies | Varies | Varies | Varies |
| [Mistral](https://learn.microsoft.com/en-us/agent-framework/integrations/by-component/model-providers/mistral) | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ |

#### 2.7.7 SDK and Endpoint Selection

Several .NET SDKs can connect to Microsoft Foundry, Azure OpenAI, OpenAI, or Anthropic. Choose the SDK that matches the service endpoint and authentication model.

| AI Service | Client SDK | NuGet Packages | Endpoint or Identifier |
| --- | --- | --- | --- |
| Microsoft Foundry project | Azure AI Projects | `Azure.AI.Projects`, `Microsoft.Agents.AI.Foundry` | `https://<resource>.services.ai.azure.com/api/projects/<project>` |
| Microsoft Foundry Models through OpenAI v1 | OpenAI | `OpenAI`, `Microsoft.Agents.AI.OpenAI` | `https://<resource>.services.ai.azure.com/openai/v1/` |
| Azure OpenAI | Azure OpenAI | `Azure.AI.OpenAI`, `Microsoft.Agents.AI.OpenAI` | `https://<resource>.openai.azure.com/` |
| Azure OpenAI through OpenAI v1 | OpenAI | `OpenAI`, `Microsoft.Agents.AI.OpenAI` | `https://<resource>.openai.azure.com/openai/v1/` |
| OpenAI | OpenAI | `OpenAI`, `Microsoft.Agents.AI.OpenAI` | Default OpenAI endpoint |
| Anthropic on Microsoft Foundry | Anthropic Foundry | `Anthropic.Foundry`, `Microsoft.Agents.AI.Anthropic` | Foundry resource name |
| Anthropic | Anthropic | `Anthropic`, `Microsoft.Agents.AI.Anthropic` | Default Anthropic endpoint |


## 3. 🔩 MIDDLE/LOW LEVEL: IMPLEMENTATION / CONFIGURATION CONCEPTS

### 3.1 ⚙️🧱 BASE CONFIGURATION

#### 3.1.1 Initial Setup & Usage Examples

Initial setup and usage examples can be found in:

```text
{RepoRoot}/Tests.DevQAProdCom.NET.AI/
├── AiAgentsTests.cs
├── AiAgentsConfigurationsTests.cs
├── AiInstructionsTests.cs
├── AiSkillsTests.cs
├── McpServersTests.cs
├── HooksTests.cs
├── OrchestrationTests.cs
└── Program.cs
```

#### 3.1.2 Authentication

Authentication methods supported by the GitHub Copilot SDK:

| Method | How to set |
|--------|-----------|
| GitHub signed-in user | `gh auth login` + `UseLoggedInUser = true` |
| OAuth GitHub App token | Pass token explicitly |
| Environment variables | `COPILOT_GITHUB_TOKEN`, `GH_TOKEN`, `GITHUB_TOKEN` |
| BYOK (bring your own key) | API keys, no GitHub auth |

Set via `CopilotClientOptionsBuilder`:

```csharp
.WithGitHubToken("<token>")      // explicit token
.WithUseLoggedInUser(true)       // use gh CLI / stored OAuth
```

> [📘 GitHub Copilot SDK authentication docs](https://github.com/github/copilot-sdk)

#### 3.1.3 Copilot CLI Installation

In order to use AIMS (Agentic Interactions Management System) for Node.js, Python, and **.NET** SDKs, the Copilot CLI is bundled as a dependency.

  <ItemGroup>
    <PackageReference Include="Microsoft.Agents.AI" />
    <PackageReference Include="Microsoft.Agents.AI.GitHub.Copilot" />
  </ItemGroup>

Advanced: override the CLI binary or connect to an external server. See the SDK README for details.

#### 3.1.4 How to Run

See `{RepoRoot}/Tests.DevQAProdCom.NET.AI/README.md` for full details.

The test project is a **dual-mode** project:

- **NUnit tests** — `Debug` / `Release` configuration
- **Console app** — `DebugAsApp` configuration

> ❗❗❗ When switching between `Debug`/`Release` and `DebugAsApp`, **clean and rebuild** the solution to avoid stale `Main` method conflicts.

##### 3.1.4.1 Run as NUnit Test (Class Method)

Each test class method is an ordinary NUnit test. Use the `Debug` configuration and run only the specific test you need, for example `AiAgentsTests.Should_Read_Write_Agent_Be_Executed` from `AiAgentsTests.cs`:

```bash
dotnet test Tests.DevQAProdCom.NET.AI --filter "FullyQualifiedName~AiAgentsTests.Should_Read_Write_Agent_Be_Executed"
```

The same pattern works for any other class and method, for example `AiAgentsConfigurationsTests.Should_AiAgentConfiguration_Be_Used_From_AiAgentMdFile_By_Configuration_Identifier`:

```bash
dotnet test Tests.DevQAProdCom.NET.AI --filter "FullyQualifiedName~AiAgentsConfigurationsTests.Should_AiAgentConfiguration_Be_Used_From_AiAgentMdFile_By_Configuration_Identifier"
```

From Visual Studio:

1. Open **Test** → **Test Explorer**.
2. Select the `Debug` or `Release` solution configuration. ❗❗❗ Do not use `DebugAsApp` for test-like runs.
3. Find the required test in the list or via the search box.
4. Right-click the test and choose **Run** (or **Debug**) to execute only that test.

##### 3.1.4.2 Run as Console App

❗❗❗ In Visual Studio switch to the `DebugAsApp` configuration and rebuild before running:

```bash
dotnet clean Tests.DevQAProdCom.NET.AI
dotnet build Tests.DevQAProdCom.NET.AI --configuration DebugAsApp --force
```

`Program.cs` supports four entry styles:

| Style | Arguments |
|-------|-----------|
| By class + method | `--className <ClassName> --methodName <MethodName>` |
| By agent name | `--agentName <AgentName> [--prompt "<prompt>"]` |
| By configuration | `--configurationIdentifier <Config> [--prompt "<prompt>"]` |
| By name + configuration | `--agentName <AgentName> --configurationIdentifier <Config> [--prompt "<prompt>"]` |

Examples:

```bash
# 1. Invoke a test method by class + method
dotnet run --project Tests.DevQAProdCom.NET.AI --configuration DebugAsApp --no-build -- --className AiAgentsTests --methodName Should_Read_Write_Agent_Be_Executed

# 2. Run an agent by name
dotnet run --project Tests.DevQAProdCom.NET.AI --configuration DebugAsApp --no-build -- --agentName answer-questions-agent --prompt "What is my favorite animal?"

# 3. Run an agent by configuration
dotnet run --project Tests.DevQAProdCom.NET.AI --configuration DebugAsApp --no-build -- --configurationIdentifier check-custom-instructions-field-answer-questions-agent --prompt "What is my favorite animal?"

# 4. Run an agent by name + configuration
dotnet run --project Tests.DevQAProdCom.NET.AI --configuration DebugAsApp --no-build -- --agentName answer-questions-agent --configurationIdentifier check-custom-instructions-field-answer-questions-agent --prompt "What is my favorite animal?"
```

The same options are available as launch profiles (`Properties/launchSettings.json`). Select the `DebugAsApp` configuration and press `F5`, or pick the required profile from the launch dropdown:

```json
{
  "profiles": {
    "Run Agent by ClassName and MethodName": {
      "commandName": "Project",
      "commandLineArgs": "--className AiAgentsTests --methodName Should_Read_Write_Agent_Be_Executed",
      "workingDirectory": "$(OutDir)"
    },
    "Run Agent by Name": {
      "commandName": "Project",
      "commandLineArgs": "--agentName answer-questions-agent --prompt \"<prompt>\"",
      "workingDirectory": "$(OutDir)"
    },
    "Run Agent from Configuration": {
      "commandName": "Project",
      "commandLineArgs": "--configurationIdentifier check-custom-instructions-field-answer-questions-agent --prompt \"<prompt>\"",
      "workingDirectory": "$(OutDir)"
    },
    "Run Agent by Name and Configuration": {
      "commandName": "Project",
      "commandLineArgs": "--agentName answer-questions-agent --configurationIdentifier check-custom-instructions-field-answer-questions-agent --prompt \"<prompt>\"",
      "workingDirectory": "$(OutDir)"
    }
  }
}
```

> ❗ `workingDirectory` must be `$(OutDir)` to avoid duplicate MCP server discovery across build output directories.


### 3.2 🛠️🔧 DETAILED CONFIGURATION

#### 3.2.1 🛠️🕹️ Setup Mode: `CopilotCli` or `Empty`

```csharp
.WithCopilotClientOptions(builder => builder
    .WithMode(CopilotClientMode.Empty))   // default for isolation
```

| Requirement | `Empty` | `CopilotCli` |
|-------------|---------|--------------|
| `BaseDirectory` or `SessionFs` | Required | Optional |
| `AvailableTools` per session | Required (builder injects isolated builtins) | Uses CLI defaults |
| Multi-user safe | ✅ Yes | ❌ No |


#### 3.2.2 🛠️🗂️ Setup Configuration Directories

A session touches three directory scopes: the client `BaseDirectory`, the `WorkingDirectory` (client and session), and the custom `Directory For Interaction Configuration Data` snapshot directory. Each one is owned by a specific builder class. `GitHubCopilotAiAgentInteractor` is only a facade — its `With...` methods forward to `CopilotClientOptionsBuilder` or `SessionConfigBuilder`.

##### 3.2.2.1 `Base Directory`

- Copilot runtime data directory (`COPILOT_HOME`), set once per `CopilotClient` by `CopilotClientOptionsBuilder.WithBaseDirectory()`.
- Default: `~/.copilot` if not set.
- In this implementation `WithBaseDirectory()` without an argument generates `<temp>/COPILOT_SESSION_BASE_DIR_<timestamp>`.
- Facade shortcuts `WithFullIsolation()` and `WithSelectiveIsolation()` also call `WithBaseDirectory()` together with `WithMode(CopilotClientMode.Empty)`.
- `GitHubCopilotClientService.DisposeAsync()` deletes `BaseDirectory` recursively — never point it at a directory that must survive the session.

```csharp
.WithBaseDirectory("<base-dir>")   // or empty for temp
```

##### 3.2.2.2 `Working Directory`

Two scopes:

| Builder | Property | Meaning |
|---------|----------|---------|
| `CopilotClientOptionsBuilder` | `WorkingDirectory` | Working dir of the Copilot runtime process. |
| `SessionConfigBuilder` | `WorkingDirectory` | Working dir of the agent session. |

Usually set both to the same directory:

```csharp
.WithWorkingDirectory("<work-dir>")   // facade: sets both client and session
```

`SessionConfigBuilder.Build()` throws when the session `WorkingDirectory` is empty and creates the directory when it does not exist.

##### 3.2.2.3 `Directory For Interaction Configuration Data`

This is the temporary configuration snapshot directory. It mirrors a `.github/` structure with only the agents, instructions, skills, hooks, and MCP servers relevant to the current interaction.

It is set through `SessionConfigBuilder.WithDirectoryForInteractionConfigurationData(string directoryPath)`. When the facade path is used, the value is first produced by `MicrosoftAiAgentInteractorT<T>.SetupDirectoryForInteractionConfigurationData()` and then passed to the builder by `GitHubCopilotAiAgentInteractor.GetAiAgentAsync()`. The facade exposes the same setter through `MicrosoftAiAgentInteractorT<T>.WithDirectoryForInteractionConfigurationData()` when the default location must be overridden. Default location: `SharedAiIoUtils.GetTempAiInterationSessiondirectory()` → `<temp>/AiInterationSession<yyyy-MM-dd_hh-mm-ss.fffffff>`.

`SessionConfigBuilder.SetupDirectoryForInteractionConfigurationData()` creates the directory and rejects a path equal to the session `WorkingDirectory` to avoid conflicts with a `.github` folder that may already exist there.

```text
<interaction-dir>/                            # WithDirectoryForInteractionConfigurationData()
├── .github/                                  # SessionConfigBuilder.Build()
│   ├── agents/                               # SaveAiAgents()
│   ├── instructions/                         # SaveAiInstructions()
│   ├── skills/                               # SaveAiSkills()
│   ├── hooks/                                # SaveHooks()
│   └── mcp-servers/                          # SaveMcpServers()
├── CopilotClientOptions.json                 # GitHubCopilotAiAgentInteractor.GetAiAgentAsync()
└── AgentRunOptions.json                      # GitHubCopilotAiAgentInteractor.GetAiAgentAsync()
```

> The two JSON files `CopilotClientOptions.json` and `AgentRunOptions.json` are written only by the facade `GetAiAgentAsync()` path, not by `SessionConfigBuilder` alone.

**Why it exists:**

1. **Audit/log** the exact configuration used for an interaction.
2. **Artificial Isolation** — exclude irrelevant agents/instructions/hooks/MCP servers from the session.

**Important — File-Based hooks:**

The GitHub Copilot SDK currently loads File-Based hooks **only** from `.github/hooks` inside the session working directory. Therefore, when File-Based hooks are configured, `SessionConfigBuilder` **fully replaces** the working directory's `.github` directory for the duration of the session. `EnableFileHooks` is set to `true` automatically.

Substitution flow:

```text
1. Build() → SetupDirectoryForInteractionConfigurationData()
        ↓
2. Save agents/instructions/skills/hooks/MCP to <interaction-dir>/.github/
        ↓
3. If File-Based hooks exist (ConfigureDataInGitHubDirectory):
        a. Rename existing <work-dir>/.github → <work-dir>/.github-initial
        b. Create an empty <work-dir>/.github
        c. Copy <interaction-dir>/.github/hooks → <work-dir>/.github/hooks
        ↓
4. SessionConfig is returned by Build()
        ↓
5. On Dispose():
        a. Delete <interaction-dir>
        b. Delete <work-dir>/.github and restore <work-dir>/.github-initial → <work-dir>/.github
```

##### 3.2.2.4 CLI File-Based Isolation using `Directory For Interaction Configuration Data`

Full isolation (`WithFullIsolation()` / `WithSelectiveIsolation()`) toggles SDK discovery flags. **Partial Artificial Isolation** is about files only: from a shared `.github/` that contains many agents, skills, hooks, and MCP servers, generate a snapshot with only what a single agent needs, temporarily put that snapshot in place of the shared `.github/`, and run that agent through the CLI (or any other runner) with only its own data visible. Those isolation flags have no effect on the CLI — what the CLI sees is only the generated `.github/` directory.

This does not require the facade, `GitHubCopilotAiAgentInteractor`, `CopilotClientOptions`, or a running `CopilotClient`. `SessionConfigBuilder` is public and its constructor needs only an `ILogger`; every other constructor parameter is optional, and default locations are discovered from the current directory and the nearest solution directory.

```csharp

{
    return new SessionConfigBuilder(logger)
        .WithWorkingDirectory(workingDirectory)
        .WithDirectoryForInteractionConfigurationData(interactionDirectory)
        .WithPrimaryAgent(agentIdentifier)
        .WithInstructions("instruction-identifier")                        // only what this agent needs
        .WithSkills("skill-a-identifier", "skill-b-identifier")            // only what this agent needs
        .WithFileBasedHooks("hook-identifier")                             // only what this agent needs
        .WithFileBasedMcpServer("mcp-identifier");                         // only what this agent needs
}
```

`Build()` writes the curated snapshot to `interactionDirectory/.github/{agents,instructions,skills,hooks,mcp-servers}`.

Keep in mind that, according to the current implementation:

- `Build()` requires a session `WorkingDirectory` and may swap `.github` there when File-Based hooks are included. When the snapshot is built only for an external CLI run, point `WithWorkingDirectory()` at a mock/temp/real working directory, and use `WithDirectoryForInteractionConfigurationData()` for the directory that receives the curated `.github`.
- `SessionConfigBuilder.Dispose()` deletes the interaction-configuration directory. Keep the builder alive for the whole CLI run and restore the shared `.github` before disposing.
- Because the snapshot contains only the selected agents/instructions/skills/hooks/MCP servers, the shared directory is untouched on disk while the agent runs, and the original state is restored afterwards.

#### 3.2.3 🛠️🤖 Configure Agents

##### 3.2.3.1 File-Based and SDK-Based Agents Examples

`<agent-identifier>.agent.md` file:

```markdown
---

name: agent-identifier                                                        # 🧩 provider CLI supported YAML property
description: agent-description                                                # 🧩 provider CLI supported YAML property

... other configuration properties
---

Agent Prompt...

```

```csharp
new CustomAgentConfig { Name = "custom-sdk-class-initialized" }
```


> **Note:** The `name` of the agent serves as its identifier.

##### 3.2.3.2 Collections, Searchers and Locations Providers

Agents live in `IAiAgentsCollectionWithConfigurations<T>` (default implementation: `AiAgentsCollectionWithConfigurations<T>`).

**Two collections are kept by `SessionConfigBuilder`:**

| Collection | Field | Purpose |
|------------|-------|---------|
| All agents (catalog) | `_allAgentsCollection` | Agents and JSON configurations discovered from default locations. `WithPrimaryAgent` / `WithAgent` resolve entities from here. |
| Session agents | `_sessionAgentsCollection` | Only the agents selected for the current session. `Build()` maps them into `SessionConfig.CustomAgents` and aggregates their tools, subagents, instructions, skills, and MCP servers. |

**Default collection behavior:**

- `GitHubCopilotAgentsSearcher` searches `*.agent.md` files (YAML front matter + Markdown prompt body).
- `GitHubCopilotAgentsDefaultLocationsProvider` supplies the default agent locations: `.github/agents` plus `*.agent.md` files under the current directory and nearest solution directory (extended search enabled).
- `AiAgentsConfigurationsSearcher` loads `.configuration.json` files as `IAiAgentConfiguration`.
- `GitHubCopilotAgentsConfigurationsDefaultLocationsProvider` supplies the default configuration locations: `.github/agents-configurations` plus `*.configuration.json` files under the current directory and nearest solution directory (extended search enabled).
- `GitHubCopilotAiAgentsConfigurationsMappers` maps an agent plus a selected configuration into a runnable agent entity, and extracts agent-level configurations into `IAiAgentConfiguration`.

**Everything injectable through the `SessionConfigBuilder` constructor:**

All agent parameters are optional; `null` means the default implementation is created.

| Parameter | Interface | Default |
|-----------|-----------|---------|
| `agentsSearcher` | `ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>` | `GitHubCopilotAgentsSearcher` |
| `agentsDefaultLocationsProvider` | `ILocationsProvider` | `GitHubCopilotAgentsDefaultLocationsProvider` |
| `agentsConfigurationsMappers` | `IAiAgentsConfigurationsMappers<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>` | `GitHubCopilotAiAgentsConfigurationsMappers` |
| `agentsConfigurationsSearcher` | `ISearchEntitiesInLocations<IAiAgentConfiguration>` | `AiAgentsConfigurationsSearcher` |
| `agentsDefaultConfigurationsLocationsProvider` | `ILocationsProvider` | `GitHubCopilotAgentsConfigurationsDefaultLocationsProvider` |
| `allAgentsCollection` | `IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>` | `AiAgentsCollectionWithConfigurations<T>` built from the five services above |
| `sessionAgentsCollection` | `IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>` | `AiAgentsCollectionWithConfigurations<T>` built from `agentsSearcher` + `agentsConfigurationsMappers` only — no default locations, so it starts empty |

```csharp
new SessionConfigBuilder(
    logger,
    agentsSearcher: <ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>>,
    agentsDefaultLocationsProvider: <ILocationsProvider>,
    agentsConfigurationsMappers: <IAiAgentsConfigurationsMappers<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>,
    agentsConfigurationsSearcher: <ISearchEntitiesInLocations<IAiAgentConfiguration>>,
    agentsDefaultConfigurationsLocationsProvider: <ILocationsProvider>,
    allAgentsCollection: <IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>,
    sessionAgentsCollection: <IAiAgentsCollectionWithConfigurations<IGitHubCopilotAiAgentConfigurationWithCustomMetadata>>);
```

> The session collection is deliberately created without a default locations provider and without a configurations searcher: it never auto-discovers files. Entries reach it only through the `WithPrimaryAgent*` / `WithAgent*` methods, which copy the resolved agent (with the selected configuration applied) from the all agents collection.

##### 3.2.3.3 Session Configuration Builder

**Primary agent vs additional agents:**

- `WithPrimaryAgent*` methods set `SessionConfig.Agent` — the name of the custom agent to activate when the session starts. It must match the `CustomAgentConfig.Name` of one of the agents in `SessionConfig.CustomAgents`. When `SessionConfig.Agent` is not set, the session runs with the default GitHub Copilot agent.
- `WithAgent*` methods (without the `Primary` prefix) only add agents to the session collection. They never set `SessionConfig.Agent`; they are mostly used to register the subagents that the primary agent can delegate to.

##### 3.2.3.4 Configuration Options

**Configuration override rule:**

> Agent-level `tools` and `model` are shared defaults. `custom-metadata.configurations[]` entries override them when the same property is present.

A configuration with an empty or `default` identifier is treated as the agent-specific default configuration and is not registered as a shared configuration; configurations with explicit identifiers are indexed by `ConfigurationIdentifier` and can be resolved with `GetByConfiguration` / `GetByIdentifierAndConfiguration` / `GetByFilePathAndConfiguration`.

###### 3.2.3.4.1 YAML File-Based

YAML: base agent properties in `agent.md`:

```yaml
---
name: agent-identifier
description: agent-description
---

Agent Prompt...

```

###### 3.2.3.4.2 JSON File-Based

JSON: base agent properties in the JSON configuration:

```json
{
  "name": "agent-identifier",
  "description": "agent-description"
}
```

###### 3.2.3.4.3 SDK-Based

SDK-Based Configuration Options:

```csharp
// Primary agent — sets SessionConfig.Agent
.WithPrimaryAgent("agent-identifier")                                            // by name, default configuration
.WithPrimaryAgent("agent-identifier", "configuration-identifier")                 // by name + configuration
.WithPrimaryAgentByConfiguration("check-custom-instructions-field")               // by configuration only
.WithPrimaryAgentFromFile("<path>.agent.md")                                      // from file, default configuration
.WithPrimaryAgentFromFile("<path>.agent.md", "configuration-identifier")

// Additional agents / subagents — do not set SessionConfig.Agent (do not set Primary Agent)
.WithAgent("sub-agent-identifier")                                                 // by name, default configuration
.WithAgent("sub-agent-identifier", "configuration-identifier")                     // by name + configuration
.WithAgentByConfiguration("configuration-identifier")                              // by configuration only
.WithAgents("subagent-identifier-1", "subagent-identifier-2")                      // by names
.WithAgentFromFile("<path>.agent.md")                                              // from file
.WithAgentsFromFiles("<path1>.agent.md", "<path2>.agent.md")                       // from files
.WithAgentsFromDirectory("<agents-dir>")                                           // from directory
.WithAgentsFromDirectories("<agents-dir-1>", "<agents-dir-2>")                     // from directories

// Raw SDK custom agent config
.WithCustomAgentConfig(new CustomAgentConfig { Name = "custom-sdk-class-initialized" })
```

---

#### 3.2.4 🛠️📋 Configure Instructions / Rules

##### 3.2.4.1 File-Based and SDK-Based Instructions Examples

`<instructions-identifier>.instructions.md` file:

```markdown
---

name: instruction-identifier                                                  # 🧩 provider CLI supported YAML property
description: instruction-description                                          # 🧩 provider CLI supported YAML property
applyTo: ["**/*.cs"]                                                          # 🧩 provider CLI supported YAML property

---

Instruction Prompt...
```

> **Note:** "Instructions" is the Copilot alternative naming for "Rules". The `name` of the instruction serves as its identifier.

##### 3.2.4.2 Collections, Searchers and Locations Providers

Instructions live in `IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>` (default implementation: `AiEntitiesCollection<T>`).

**Two collections are kept by `SessionConfigBuilder`:**

| Collection | Field | Purpose |
|------------|-------|---------|
| All instructions (catalog) | `_allInstructionsCollection` | Instructions discovered from default locations. `WithInstruction("<identifier>")` resolves entries from here. |
| Session instructions | `_sessionInstructionsCollection` | Only the instructions selected for the current session. `Build()` writes them to `<interaction-dir>/.github/instructions` and aggregates the instructions referenced by the session agents. |

**Default collection behavior:**

- `GitHubCopilotInstructionsSearcher` searches `*.instructions.md` files (YAML front matter `name`, `description`, optional `applyTo` + Markdown prompt body).
- `GitHubCopilotInstructionsDefaultLocationsProvider` supplies the default locations: `.github/instructions` plus `*.instructions.md` files under the current directory and nearest solution directory (extended search enabled).
- Inline instructions created with `WithInstruction("<identifier>", "<prompt>")` have no file path and are saved as `<identifier>.instructions.md` in the interaction snapshot.

**Everything injectable through the `SessionConfigBuilder` constructor:**

All instruction parameters are optional; `null` means the default implementation is created.

| Parameter | Interface | Default |
|-----------|-----------|---------|
| `instructionsSearcher` | `ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiInstructionConfiguration>>` | `GitHubCopilotInstructionsSearcher` |
| `instructionsDefaultLocationsProvider` | `ILocationsProvider` | `GitHubCopilotInstructionsDefaultLocationsProvider` |
| `allInstructionsCollection` | `IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>` | `AiEntitiesCollection<T>` built from the searcher + default locations provider |
| `sessionInstructionsCollection` | `IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>` | `AiEntitiesCollection<T>` built only from the searcher — no default locations, so it starts empty |

```csharp
new SessionConfigBuilder(
    logger,
    instructionsSearcher: <ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiInstructionConfiguration>>>,
    instructionsDefaultLocationsProvider: <ILocationsProvider>,
    allInstructionsCollection: <IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>>,
    sessionInstructionsCollection: <IAiEntitiesCollection<IGitHubCopilotAiInstructionConfiguration>>);
```

> The session collection never auto-discovers files. Entries reach it through the `WithInstruction*` methods and during `Build()`, which copies every instruction referenced by the configured agents (`custom-metadata.configurations[].instructions`) from the all instructions collection.

##### 3.2.4.3 Session Configuration Builder

**`Build()` wiring:**

`ConfigureInstructions()`:

1. Collects `custom-metadata.configurations[].instructions` from all session agents, resolves each identifier in the all instructions collection, and adds the resolved instruction to the session instructions collection.
2. Writes the session instructions to `<interaction-dir>/.github/instructions`.
3. Sets `SessionConfig.InstructionDirectories` to that snapshot directory when it is not already set.

> `WithInstructionDirectories()` sets `SessionConfig.InstructionDirectories` directly. The current `ConfigureInstructions()` implementation expects this property to be unset and throws when it is already populated — prefer the `WithInstruction*` methods and let `Build()` point `InstructionDirectories` at the snapshot.

##### 3.2.4.4 Configuration Options

###### 3.2.4.4.1 YAML File-Based

YAML: instructions can be referenced in `agent.md` through `custom-metadata.configurations[].instructions`:

```yaml
custom-metadata:
  configurations:
    - configurationIdentifier: configuration-identifier
      instructions: ["instruction-identifier-1", "instruction-identifier-2"]
```

###### 3.2.4.4.2 JSON File-Based

JSON: instructions can be referenced in the JSON configuration:

```json
{
  "configurationIdentifier": "configuration-identifier",
  "instructions": [ "instruction-identifier-1", "instruction-identifier-2" ]
}
```

###### 3.2.4.4.3 SDK-Based

SDK-Based Configuration Options:

```csharp
// From the all instructions collection, by identifier (YAML `name`)
.WithInstruction("instruction-identifier-1")
.WithInstructions("instruction-identifier-1", "instruction-identifier-2")

// Inline instruction: identifier + prompt, no file required
.WithInstruction("inline-rule", "Always write tests first.")

// From files and directories
.WithInstructionFromFile("<path>.instructions.md")
.WithInstructionsFromFiles("<path1>.instructions.md", "<path2>.instructions.md")
.WithInstructionsFromDirectory("<instructions-dir>")
.WithInstructionsFromDirectories("<instructions-dir-1>", "<instructions-dir-2>")

// Sets SessionConfig.InstructionDirectories directly
.WithInstructionDirectories("<dir1>", "<dir2>")
```

---

#### 3.2.5 🛠️🎓 Configure Skills

##### 3.2.5.1 File-Based and SDK-Based Skills Examples

`<skill-identifier>/SKILL.md` file:

```markdown
---

name: skill-identifier                                                         # 🧩 provider CLI supported YAML property
description: skill-description                                                 # 🧩 provider CLI supported YAML property
allowed-tools: ["view", "create"]                                              # 🧩 provider CLI supported YAML property

---

Skill Prompt...
```
> **Note:** The `name` of the skill serves as its identifier.

##### 3.2.5.2 Collections, Searchers and Locations Providers

Skills live in `IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>` (default implementation: `AiEntitiesCollection<T>`).

**Two collections are kept by `SessionConfigBuilder`:**

| Collection | Field | Purpose |
|------------|-------|---------|
| All skills (catalog) | `_allSkillsCollection` | Skills discovered from default locations. `WithSkill("<identifier>")` resolves entries from here. |
| Session skills | `_sessionSkillsCollection` | Only the skills selected for the current session. `Build()` copies them into `<interaction-dir>/.github/skills` and aggregates the skills referenced by the session agents. |

**Default collection behavior:**

- `GitHubCopilotSkillsSearcher` searches `SKILL.md` files (YAML front matter `name`, `description`, optional `allowed-tools` + Markdown prompt body).
- `GitHubCopilotSkillsDefaultLocationsProvider` supplies the default locations: `.github/skills` plus `SKILL.md` files under the current directory and nearest solution directory (extended search enabled).
- Inline skills created with `WithSkill("<identifier>", "<prompt>")` have no file path and are saved as `<interaction-dir>/.github/skills/<identifier>/SKILL.md`.

**Everything injectable through the `SessionConfigBuilder` constructor:**

All skill parameters are optional; `null` means the default implementation is created.

| Parameter | Interface | Default |
|-----------|-----------|---------|
| `skillsSearcher` | `ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>>` | `GitHubCopilotSkillsSearcher` |
| `skillsDefaultLocationsProvider` | `ILocationsProvider` | `GitHubCopilotSkillsDefaultLocationsProvider` |
| `allSkillsCollection` | `IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>` | `AiEntitiesCollection<T>` built from the searcher + default locations provider |
| `sessionSkillsCollection` | `IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>` | `AiEntitiesCollection<T>` built only from the searcher — no default locations, so it starts empty |

```csharp
new SessionConfigBuilder(
    logger,
    skillsSearcher: <ISearchEntitiesInLocations<IAiEntityWithTConfigurationType<IGitHubCopilotAiSkillConfiguration>>>,
    skillsDefaultLocationsProvider: <ILocationsProvider>,
    allSkillsCollection: <IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>>,
    sessionSkillsCollection: <IAiEntitiesCollection<IGitHubCopilotAiSkillConfiguration>>);
```

> The session collection never auto-discovers files. Entries reach it through the `WithSkill*` methods and during `Build()`, which copies every skill referenced by the configured agents (`custom-metadata.configurations[].skills`) from the all skills collection.

##### 3.2.5.3 Session Configuration Builder

**`Build()` wiring:**

`ConfigureSkills()`:

1. Collects `custom-metadata.configurations[].skills` from all session agents, resolves each identifier in the all skills collection, and adds the resolved skill to the session skills collection.
2. Writes the session skills to `<interaction-dir>/.github/skills` — a skill with a file path is copied as its whole source directory, an inline skill is written as `<identifier>/SKILL.md`.
3. Sets `SessionConfig.SkillDirectories` to the individual skill directories under the snapshot when it is not already set.
4. Adds the snapshot skill directory names to the primary agent's `CustomAgentConfig.Skills` (the primary agent is resolved via `SessionConfig.Agent`), and mirrors the same names on the primary agent entity in the session skills collection.

> Skills are copied as full directories with all related files, so source skill directory names must be unique — `SaveAiSkills()` throws when the destination directory already exists.
>
> `WithSkillDirectories()` sets `SessionConfig.SkillDirectories` directly; the existing value is kept and step 3 is skipped, but step 4 still applies its directory names to the primary agent's `CustomAgentConfig.Skills`.

##### 3.2.5.4 Configuration Options

###### 3.2.5.4.1 YAML File-Based

Skills can be referenced in `agent.md` through `custom-metadata.configurations[].skills`:

```yaml
custom-metadata:
  configurations:
    - configurationIdentifier: configuration-identifier
      skills: ["skill-identifier-1", "skill-identifier-2"]
```

###### 3.2.5.4.2 JSON File-Based

Skills can be referenced in the JSON configuration:

```json
{
  "configurationIdentifier": "configuration-identifier",
  "skills": [ "skill-identifier-1", "skill-identifier-2" ]
}
```

###### 3.2.5.4.3 SDK-Based

SDK-Based Configuration Options:

```csharp
// From the all skills collection, by identifier (YAML `name`)
.WithSkill("skill-identifier")
.WithSkills("skill-identifier-1", "skill-identifier-2")

// Inline skill: identifier + prompt, no file required
.WithSkill("inline-skill-identifier", "<prompt body>")

// From files and directories
.WithSkillFromFile("<skill-dir>/SKILL.md")
.WithSkillsFromFiles("<skill-dir-1>/SKILL.md", "<skill-dir-2>/SKILL.md")
.WithSkillsFromDirectory("<skills-dir>")
.WithSkillsFromDirectories("<skills-dir-1>", "<skills-dir-2>")

// Sets SessionConfig.SkillDirectories directly
.WithSkillDirectories("<skills-dir-1>", "<skills-dir-2>")
```

---

#### 3.2.6 🛠️🪝 Configure Hooks

##### 3.2.6.1 File-Based and SDK-Based Hooks Examples

Hooks come in two independent types, each with its own catalog and session collection.

**Two hook types:**

| Type | Source | Loader |
|------|--------|--------|
| File-Based | `.github/hooks/*.json` | SDK reads from `.github/hooks` only |
| SDK-Based | C# code | `SessionHooksBuilder` |


File-Based `.json` hook:

```json
{
  "hooks": {
    "sessionStart": [
      {
        "custom-metadata": {
          "identifier": "on-session-start-hook-1",
          "data": [
            {
              "directory": "scripts",
              "files": [
                "scripts/on-session-start-hook-1.ps1"
              ]
            }
          ]
        },
        "type": "command",
        "powershell": ".github/hooks/scripts/on-session-start-hook-1.ps1",
        "timeoutSec": 30
      }
    ]
  }
}
```

SDK-Based `.cs` hook:

```csharp
    public class GitHubCopilotSdkBasedOnSessionStartHook1 : GitHubCopilotSdkBasedOnSessionStartHook
    {
        public override string? Identifier { get; set; } = $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)}";

        public override Func<SessionStartHookInput, HookInvocation, Task<SessionStartHookOutput?>>? GetOnSessionStartHook()
        {
            return async (input, invocation) =>
            {
                var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                var content = $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)} {timestamp}";
                var outputDirectory = input.WorkingDirectory ?? Directory.GetCurrentDirectory();
                IoUtils.WriteAllText(Path.Combine(outputDirectory, $"{nameof(GitHubCopilotSdkBasedOnSessionStartHook1)}.txt"), content);
                return await Task.FromResult<SessionStartHookOutput?>(null);
            };
        }
    }
```

##### 3.2.6.2 Collections, Searchers and Locations Providers

**Collections kept by `SessionConfigBuilder`:**

| Collection | Field | Purpose |
|------------|-------|---------|
| All File-Based hooks (catalog) | `_allFileBasedHooksCollection` | File-Based hooks discovered from default locations. `WithFileBasedHook("<identifier>")` resolves entries from here. |
| Session File-Based hooks | `_sessionFileBasedHooksCollection` | Only the File-Based hooks selected for the session. `Build()` writes them into `<interaction-dir>/.github/hooks` and substitutes them into the working directory `.github` when File-Based hooks are used. |
| All SDK-Based hooks (catalog) | `_allSdkBasedSessionHooksCollection` | SDK hook objects supplied by the caller and resolved by identifier. Empty by default — inject it through the constructor or the facade factory. |
| Session SDK-Based hooks | `_sessionSdkBasedSessionHooksCollection` | Only the SDK hooks selected for the session. `Build()` applies each hook via `AddTo(SessionConfig.Hooks)`. |

**Default collection behavior:**

- `GitHubCopilotFileBasedHooksSearcher` parses `.github/hooks/*.json`: each `hooks.<event-trigger-name>[]` entry becomes an `IFileBasedHook`; optional `custom-metadata` provides `identifier`, `description`, and `data` and is removed from the provider content.
- `GitHubCopilotFileBasedHooksDefaultLocationsProvider` supplies the default locations: `.github/hooks` plus `*.hooks.json` files under the current directory and nearest solution directory (extended search enabled).
- `FileBasedHooksCollection` validates every hook on `Add`: `EventTriggerName` and `ContentValue` are required, a programmatic hook without a file path must have an identifier, and hook identifiers / file-path + event-trigger combinations must be unique.
- SDK-Based collections are plain `IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>`; a hook is any C# object implementing `IGitHubCopilotSdkBasedSessionHook` (`Identifier`, `Description`, `AddTo(SessionHooks)`).

**Everything injectable through the `SessionConfigBuilder` constructor:**

All hook parameters are optional; `null` means the default implementation is created.

| Parameter | Interface | Default |
|-----------|-----------|---------|
| `fileBasedHooksSearcher` | `IFileBasedHooksSearcher` | `GitHubCopilotFileBasedHooksSearcher` |
| `fileBasedHooksDefaultLocationsProvider` | `ILocationsProvider` | `GitHubCopilotFileBasedHooksDefaultLocationsProvider` |
| `allFileBasedHooksCollection` | `IFileBasedHooksCollection` | `FileBasedHooksCollection` built from the searcher + default locations provider |
| `sessionFileBasedHooksCollection` | `IFileBasedHooksCollection` | `FileBasedHooksCollection` built only from the searcher — no default locations, so it starts empty |
| `allSdkBasedSessionHooksCollection` | `IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>` | `IdentifierBasedEntitiesCollection<T>` — empty, caller-populated |
| `sessionSdkBasedSessionHooksCollection` | `IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>` | `IdentifierBasedEntitiesCollection<T>` — empty |

```csharp
new SessionConfigBuilder(
    logger,
    fileBasedHooksSearcher: <IFileBasedHooksSearcher>,
    fileBasedHooksDefaultLocationsProvider: <ILocationsProvider>,
    allFileBasedHooksCollection: <IFileBasedHooksCollection>,
    sessionFileBasedHooksCollection: <IFileBasedHooksCollection>,
    allSdkBasedSessionHooksCollection: <IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>>,
    sessionSdkBasedSessionHooksCollection: <IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedSessionHook>>);
```

> The session File-Based collection never auto-discovers files: entries reach it through the `WithFileBasedHook*` methods and during `Build()` from the hook identifiers referenced by the session agents. The SDK catalogs are not discovered at all — `allSdkBasedSessionHooksCollection` must be populated by the caller (for example an `IdentifierBasedEntitiesCollection<T>` with hooks added), otherwise `WithSdkBasedHook("<identifier>")` cannot resolve anything.

##### 3.2.6.3 Session Configuration Builder

**`Build()` wiring:**

`ConfigureHooks()`:

1. Collects `custom-metadata.configurations[].hooks` from all session agents and calls `WithHooks(identifiers)`, which resolves each identifier in both catalogs and adds it to the corresponding session collection (both are added when the identifier exists in both).
2. Calls `SaveHooks()`, which writes the session File-Based hooks into `<interaction-dir>/.github/hooks`:
   - hooks that come from a file keep their source file name, and duplicate destination file names throw;
   - hooks added programmatically with JSON string/no file path are written as `<identifier>.hooks.json`, and duplicate identifiers throw.
   - SDK-Based hooks are not serializable and are not saved; the method that adds them by identifier only logs them.
3. Builds `SessionConfig.Hooks` from `SessionHooksBuilder` when no `SessionHooks` was assigned directly, then applies every session SDK hook via `AddTo()`.
4. `ConfigureDataInGitHubDirectory()`: when File-Based hooks exist, it sets up the working directory `.github` and copies the snapshot `.github/hooks` into it, automatically setting `EnableFileHooks = true` when it was not set.

> If File-Based hooks are used, the `.github` directory substitution mechanism from section [3.2.2.4](#3224-cli-file-based-isolation-using-directory-for-interaction-configuration-data) is mandatory.

##### 3.2.6.4 Configuration Options

###### 3.2.6.4.1 YAML File-Based

Hooks can be referenced in agent `custom-metadata.configurations[].hooks` — the same identifier space resolves both types:

```yaml
custom-metadata:
  configurations:
    - configurationIdentifier: configuration-identifier
      hooks: ["hook-identifier", "sdk-hook-identifier"]
```

###### 3.2.6.4.2 JSON File-Based

Hooks can be referenced in the JSON configuration:

```json
{
  "configurationIdentifier": "configuration-identifier",
  "hooks": [ "hook-identifier-1", "hook-identifier-2" ]
}
```

###### 3.2.6.4.3 SDK-Based

SDK-Based Configuration Options:

```csharp
// File-Based: `.github/hooks/*.json` and `*.hooks.json` locations
.WithEnableFileHooks(true)
.WithFileBasedHook("hook-identifier")
.WithFileBasedHooks("hook-identifier-1", "hook-identifier-2")
.WithFileBasedHooksFromFile("<path>.hooks.json")
.WithFileBasedHooksFromFiles("<path1>.hooks.json", "<path2>.hooks.json")
.WithFileBasedHooksFromDirectory("<hooks-dir>")
.WithFileBasedHooksFromDirectories(useExtendedSearch: true, "<hooks-dir-1>", "<hooks-dir-2>")

// File-Based or SDK-Based, resolved by identifier in both catalogs
.WithHooks("hook-identifier")
.WithHooks("hook-identifier-1", "hook-identifier-2")

// SDK-Based: catalog lookup
.WithSdkBasedHook("sdk-hook-identifier")
.WithSdkBasedHooks("sdk-hook-identifier-1", "sdk-hook-identifier-2")

// SDK-Based: direct handlers through SessionHooksBuilder
.WithSdkBasedHooks(h => h
    .WithOnSessionStart(async (s, e) => { /* ... */ })
    .WithOnPreToolUse(async (s, e) => { /* ... */ })
    .WithOnPostToolUse(async (s, e) => { /* ... */ }))
```

`SessionHooksBuilder` handlers: `WithOnSessionStart`, `WithOnSessionEnd`, `WithOnUserPromptSubmitted`, `WithOnPreToolUse`, `WithOnPreMcpToolCall`, `WithOnPostToolUse`, `WithOnPostToolUseFailure`, `WithOnErrorOccurred`, plus `WithSessionHooks(Func<SessionHooks, SessionHooks>)` and `WithSessionHook(IGitHubCopilotSdkBasedSessionHook)`.

> The programmatic overloads `WithFileBasedHook("<identifier>", "<hook-json>")` and `WithFileBasedHook<T>("<identifier>", "<hook>")` currently create a `HookModel` without `EventTriggerName`, which `FileBasedHooksCollection.Add()` requires — prefer the JSON file / directory methods for File-Based hooks.

---

#### 3.2.7 🛠️🔌 Configure MCP Servers

##### 3.2.7.1 File-Based and SDK-Based MCP Servers Examples

MCP servers come in two independent types, each with its own catalog and session collection.

**Two MCP server types:**

| Type | Source | Example |
|------|--------|---------|
| File-Based | `.github/mcp.json` or `.github/mcp-servers/*.mcp.json` | `playwright-mcp` |
| SDK-Based | C# classes implementing `IGitHubCopilotSdkBasedMcpServer` | `GitHubCopilotPlaywrightMcpServer` |

File-Based `.mcp.json` from `.github/mcp.json` or `.github/mcp-servers/`:

```json
{
  "mcp-server-identifier": {
    "type": "stdio",
    "command": "<command>",
    "args": ["<argument>"],
    "tools": ["*"]
  }
}
```

SDK-Based `.cs` MCP server:

```csharp
public class GitHubCopilotPlaywrightMcpServer : GitHubCopilotSdkBasedStdioMcpServer
{
    public override string? Identifier { get; set; } = SharedAiConstants.McpServers.Identifiers.PLAYWRIGHT;

    public GitHubCopilotPlaywrightMcpServer(ILogger logger)
    {
        McpServerConfiguration = new McpStdioServerConfigBuilder(logger)
            .WithCommand("npx")
            .WithArgs("@playwright/mcp@latest")
            .WithTools("*")
            .Build();
    }
}
```

##### 3.2.7.2 Collections, Searchers and Locations Providers

**Collections kept by `SessionConfigBuilder`:**

| Collection | Field | Purpose |
|------------|-------|---------|
| All File-Based MCP servers (catalog) | `_allFileBasedMcpServersCollection` | File-Based MCP servers discovered from default locations. `WithFileBasedMcpServer("<identifier>")` resolves entries from here. |
| Session File-Based MCP servers | `_sessionFileBasedMcpServersCollection` | Only the File-Based MCP servers selected for the session. `Build()` writes them into `<interaction-dir>/.github/mcp-servers` and maps them into `SessionConfig.McpServers`. |
| All SDK-Based MCP servers (catalog) | `_allSdkBasedMcpServersCollection` | SDK MCP server objects supplied by the caller and resolved by identifier. Empty by default — inject it through the constructor or the facade factory. |
| Session SDK-Based MCP servers | `_sessionSdkBasedMcpServersCollection` | Only the SDK MCP servers selected for the session. `Build()` applies each server via `AddTo(SessionConfig.McpServers)`. |

**Default collection behavior:**

- `GitHubCopilotFileBasedMcpServersSearcher` parses both shapes: a top-level `mcpServers` object or a direct identifier-keyed object. The identifier is the server property name; `custom-metadata.description` supplies the description; supported `type` values are `stdio`/`local` and `http`/`sse`.
- `GitHubCopilotFileBasedMcpServersDefaultLocationsProvider` supplies the default locations: `.github/mcp.json`, `.github/mcp-servers/*.mcp.json`, plus `*mcp.json` files under the current directory (extended search enabled). The nearest solution directory is scanned with extended search disabled to avoid duplicates from preinstalled CLI MCP servers.
- `FileBasedMcpServersCollection` rejects duplicate identifiers and supports `AddFromFile`, `AddFromFiles`, `AddFromDirectory`, and `AddFromDirectories`; `ContentValue` is validated later, when the server is mapped or saved.
- SDK-Based collections are plain `IdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>`; a server is any C# class implementing `IGitHubCopilotSdkBasedMcpServer` (`Identifier`, `Description`, `AddTo(IDictionary<string, McpServerConfig>)`, `ToJson()`). Built-in base classes: `GitHubCopilotSdkBasedStdioMcpServer` and `GitHubCopilotSdkBasedHttpMcpServer`.

**Everything injectable through the `SessionConfigBuilder` constructor:**

All MCP server parameters are optional; `null` means the default implementation is created.

| Parameter | Interface | Default |
|-----------|-----------|---------|
| `fileBasedMcpServersSearcher` | `IFileBasedMcpServersSearcher` | `GitHubCopilotFileBasedMcpServersSearcher` |
| `fileBasedMcpServersDefaultLocationsProvider` | `ILocationsProvider` | `GitHubCopilotFileBasedMcpServersDefaultLocationsProvider` |
| `allFileBasedMcpServersCollection` | `IFileBasedMcpServersCollection` | `FileBasedMcpServersCollection` built from the searcher + default locations provider |
| `sessionFileBasedMcpServersCollection` | `IFileBasedMcpServersCollection` | `FileBasedMcpServersCollection` built only from the searcher — no default locations, so it starts empty |
| `allSdkBasedMcpServersCollection` | `IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>` | `IdentifierBasedEntitiesCollection<T>` — empty, caller-populated |
| `sessionSdkBasedMcpServersCollection` | `IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>` | `IdentifierBasedEntitiesCollection<T>` — empty |

```csharp
new SessionConfigBuilder(
    logger,
    fileBasedMcpServersSearcher: <IFileBasedMcpServersSearcher>,
    fileBasedMcpServersDefaultLocationsProvider: <ILocationsProvider>,
    allFileBasedMcpServersCollection: <IFileBasedMcpServersCollection>,
    sessionFileBasedMcpServersCollection: <IFileBasedMcpServersCollection>,
    allSdkBasedMcpServersCollection: <IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>>,
    sessionSdkBasedMcpServersCollection: <IIdentifierBasedEntitiesCollection<IGitHubCopilotSdkBasedMcpServer>>);
```

> The session File-Based collection never auto-discovers files: entries reach it through the `WithFileBasedMcpServer*` methods and during `Build()` from the MCP server identifiers referenced by the session agents. The SDK catalogs are not discovered at all — `allSdkBasedMcpServersCollection` must be populated by the caller (for example an `IdentifierBasedEntitiesCollection<T>` with servers added), otherwise `WithSdkBasedMcpServer("<identifier>")` cannot resolve anything.


##### 3.2.7.3 Session Configuration Builder

**`Build()` wiring:**

`ConfigureMcpServers()`:

1. Collects `custom-metadata.configurations[].mcpServers` from all session agents and adds each identifier to the session File-Based or SDK-Based collection (skipped when already present).
2. Throws when the same identifier is present in both session collections — identifiers must be unique across File-Based and SDK-Based MCP servers.
3. Maps session File-Based servers into `SessionConfig.McpServers[identifier]` with `GitHubCopilotMappers.ToMcpServerConfig()`, then applies every session SDK server via `AddTo(SessionConfig.McpServers)`.
4. Calls `SaveMcpServers()`, which writes the session servers into `<interaction-dir>/.github/mcp-servers`:
   - File-Based servers are grouped by source file path and written as identifier-keyed objects into `<source-file-name>.json`; servers without a file path (inline JSON/typed) go into `file-based-mcp-servers.json`;
   - SDK-Based servers are serialized through `ToJson()` into `<identifier>.json`.

> During `ConfigureAgents()`, each agent's `custom-metadata.configurations[].mcpServers` identifiers are also resolved into its `CustomAgentConfig.McpServers`; a missing identifier or an identifier found in both catalogs throws.

##### 3.2.7.4 Configuration Options

###### 3.2.7.4.1 YAML File-Based
 
MCP servers can be referenced in agent `custom-metadata.configurations[].mcpServers` — the same identifier space resolves both types:

```yaml
custom-metadata:
  configurations:
    - configurationIdentifier: configuration-identifier
      mcpServers: ["file-based-mcp-server-identifier", "sdk-based-mcp-server-identifier"]
```

###### 3.2.7.4.2 JSON File-Based
 
MCP servers can be referenced in the JSON configuration:

```json
{
  "configurationIdentifier": "configuration-identifier",
  "mcpServers": [ "file-based-mcp-server-identifier", "sdk-based-mcp-server-identifier" ]
}
```

###### 3.2.7.4.3 SDK-Based

```csharp
// File-Based: `.github/mcp.json` and `.github/mcp-servers/*.mcp.json` locations
.WithFileBasedMcpServer("mcp-server-identifier")
.WithFileBasedMcpServers("mcp-server-identifier-1", "mcp-server-identifier-2")
.WithFileBasedMcpServersFromFile("<path>.mcp.json")
.WithFileBasedMcpServersFromFiles("<path1>.mcp.json", "<path2>.mcp.json")
.WithFileBasedMcpServersFromDirectory("<mcp-servers-dir>")
.WithFileBasedMcpServersFromDirectories(useExtendedSearch: true, "<mcp-servers-dir-1>", "<mcp-servers-dir-2>")

// File-Based or SDK-Based, resolved by identifier in both catalogs
.WithMcpServer("mcp-server-identifier")
.WithMcpServers("mcp-server-identifier-1", "mcp-server-identifier-2")

// SDK-Based: catalog lookup
.WithSdkBasedMcpServer("sdk-mcp-server-identifier")
.WithSdkBasedMcpServers("sdk-mcp-server-identifier-1", "sdk-mcp-server-identifier-2")

// Inline JSON / typed object (written as file-based-mcp-servers.json in the snapshot)
.WithFileBasedMcpServer("inline-mcp-server-identifier", "{ \"type\": \"stdio\", ... }")
.WithFileBasedMcpServer("typed-mcp-server-identifier", new { type = "stdio", command = "<command>" })
```

> `WithMcpServer` / `WithMcpServers` throw when the identifier is missing in both catalogs or present in both catalogs.

---


#### 3.2.8 🛠️🚦 Configure Permissions

Example architecture for GitHub Copilot: permissions are `IGitHubCopilotPermissionDecision` functions. Agents/configuration files only declare identifiers; `SessionConfigBuilder` resolves them into session-wide decisions evaluated by `SessionConfig.OnPermissionRequest`.

##### 3.2.8.1 Decision Contract and Evaluation

```csharp
public interface IGitHubCopilotPermissionDecision : IHaveStringIdentifier
{
    Func<PermissionRequest, PermissionInvocation, Task<PermissionDecision?>> PermissionDecisionFunc { get; set; }
}
```

| Result | Behavior |
|--------|----------|
| Non-null `PermissionDecision` | Request handled; iteration stops |
| `null` | Decision does not apply; next decision runs |
| Exception | Logged with the identifier; next decision runs |
| All `null` / empty collection | `PermissionDecision.Reject("Review the available tools...")` |

> Deny-by-default: no matching decision means rejection of the permission request.

##### 3.2.8.2 Built-in Decisions

Registered by `GitHubCopilotDefaultPermissionDecisionsCollection`; identifiers come from the `Permission` enum `[Description]` values.

| Identifier | Factory | Filter | Decision |
|------------|---------|--------|----------|
| `approve-read-view-all` | `ReadViewPermissionDecisions.GetApproveReadViewAllPermission()` | `Kind == "read"` | `ApproveOnce()` |
| `deny-read-view-all` | `ReadViewPermissionDecisions.GetDenyReadViewAllPermission()` | `Kind == "read"` | `Reject(...)` |
| `approve-write-create-all` | `WriteCreatePermissionDecisions.GetApproveWriteCreateAllPermission()` | `Kind == "write"` | `ApproveOnce()` |
| `deny-write-create-all` | `WriteCreatePermissionDecisions.GetDenyWriteCreateAllPermission()` | `Kind == "write"` | `Reject(...)` |
| `approve-write-edit-all` | `WriteEditPermissionDecisions.GetApproveWriteEditAllPermission()` | `Kind == "edit"` | `ApproveOnce()` |
| `deny-write-edit-all` | `WriteEditPermissionDecisions.GetDenyWriteEditAllPermission()` | `Kind == "edit"` | `Reject(...)` |
| `approve-playwright-mcp-tools-all` | `PlaywrightMcpPermissionDecisions.GetApprovePlaywrightMcpAllPermission()` | `Kind == "mcp"`, `ServerName == "playwright"`, any tool | `ApproveOnce()` |
| `approve-filesystem-mcp-tools-all` | `FilesystemMcpPermissionDecisions.GetApproveFilesystemMcpAllPermission()` | `Kind == "mcp"`, `ServerName == "filesystem-sdk-based-mcp-server"`, any tool | `ApproveOnce()` |

- `McpPermissionDecisions.McpPermissionCheck()` requires `Kind == "mcp"`, casts to `PermissionRequestMcp`, compares `ServerName` and `ToolName` case-insensitively; `"*"` matches any tool.
- Playwright/filesystem built-ins match fixed server names; custom MCP identifiers need a custom decision.
- `GetDenyPlaywrightMcpAllPermission()` exists but is not registered; there is no built-in filesystem deny.
- Read view checks only `Kind`, not the tool name.

##### 3.2.8.3 Configuration Options

`permissions` is mapped into `IAiAgentConfigurationWithoutAiEntityIdentifiers.Permissions` by `GitHubCopilotAiAgentsConfigurationsMappers`.

###### 3.2.8.3.1 YAML File-Based

Permissions can be referenced in agent `custom-metadata.configurations[].permissions` — the same identifier space resolves built-in decisions and custom decisions injected through `allPermissionDecisionsCollection`:

```yaml
custom-metadata:
  configurations:
    - configurationIdentifier: configuration-identifier
      permissions: ["approve-read-view-all", "approve-write-create-all"]
```

###### 3.2.8.3.2 JSON File-Based

Permissions can be referenced in the JSON configuration:

```json
{
  "configurationIdentifier": "filesystem-mcp-json-file-configuration",
  "permissions": [ "approve-filesystem-mcp-tools-all" ]
}
```

###### 3.2.8.3.3 SDK-Based

| Method | Behavior |
|--------|----------|
| `SetPermission(identifier, func)` | Upserts into the session collection; an existing identifier keeps its position and gets a new function |
| `WithPermission(identifier)` | Resolves from the all-decisions catalog; throws `KeyNotFoundException` if missing |
| `WithPermissions(params string[]?)` | Repeated `WithPermission` |
| `ConfigurePermissions(directory)` | Build-time: aggregates agent `Permissions`, applies `Distinct()`, calls `WithPermissions` |
| `ConfigureOnPermissionRequest()` | Wires `SessionConfig.OnPermissionRequest`; logs, evaluates, returns the first non-null decision |
| `WithPermissionRequestsLogFile(path)` | Appends every request to the file before evaluation |


```csharp
.WithPermission("approve-read-view-all")
.WithPermissions("approve-write-create-all", "approve-filesystem-mcp-tools-all")
.SetPermission("approve-custom-filesystem-tool", (request, invocation) =>
{
    if (request is PermissionRequestMcp mcpRequest &&
        string.Equals(mcpRequest.ServerName, "filesystem-sdk-based-mcp-server", StringComparison.OrdinalIgnoreCase) &&
        string.Equals(mcpRequest.ToolName, "read_text_file", StringComparison.OrdinalIgnoreCase))
        return Task.FromResult<PermissionDecision?>(PermissionDecision.ApproveOnce());

    return Task.FromResult<PermissionDecision?>(null);
})
.WithPermissionRequestsLogFile("<path-to-permission-requests>.json")
```

> The selected configuration's `permissions` list is authoritative; a named configuration replaces the default agent permissions. `ToCustomAgentConfig()` does not put permissions on `CustomAgentConfig`; they remain session-wide. `mcpServers` and `permissions` are separate namespaces. `WithPermission` / `WithPermissions` throw `KeyNotFoundException` when an identifier is missing from the all-permissions catalog.

##### 3.2.8.4 Collections and Injection

| Collection | Field | Default |
|------------|-------|---------|
| All decisions | `_allPermissionDecisionsCollection` | `GitHubCopilotDefaultPermissionDecisionsCollection` |
| Session decisions | `_sessionPermissionDecisionsCollection` | Empty `IdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>` |

```csharp
new SessionConfigBuilder(
    logger,
    allPermissionDecisionsCollection: <IIdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>>,
    sessionPermissionDecisionsCollection: <IIdentifierBasedEntitiesCollection<IGitHubCopilotPermissionDecision>>);
```

Both parameters are forwarded through `IMicrosoftAgentFrameworkAiAgentInteractorsFactory.GetGitHubCopilotAiAgentInteractor(...)` → `MicrosoftAgentFrameworkAiAgentInteractorsFactory` → `GitHubCopilotAiAgentInteractor` → `SessionConfigBuilder`.

- Injecting `allPermissionDecisionsCollection` replaces the defaults; extend them via `new GitHubCopilotDefaultPermissionDecisionsCollection(logger)` + `Add(...)`.
- `IdentifierBasedEntitiesCollection.Add` rejects duplicates; `SetPermission` upserts.


##### 3.2.8.5 Build Flow and Ordering

`Build()` runs `ConfigurePermissions()` after `ConfigureMcpServers()` and before `ConfigureOnPermissionRequest()`:

1. `ConfigureAgents()` adds the primary agent, agents, and subagents to `_sessionAgentsCollection`.
2. `ConfigurePermissions()` collects `Permissions` from all session agents, applies `Distinct()`, and calls `WithPermission()` for each to set session-wide permission handlers.
3. `WithPermission()` resolves the function from `_allPermissionDecisionsCollection`; `SetPermission()` adds it to the session collection.
4. `ConfigureOnPermissionRequest()` sets the callback, which evaluates the session collection in insertion order and approves or rejects the permission request. If no permission handler allows it, the interaction is rejected by default.

- `.WithSessionConfig(builder => ...)` runs immediately, so explicit `SetPermission` entries precede Build-added decisions.
- If a custom `SetPermission` reuses an identifier declared in the agent configuration, Build overwrites its function with the built-in one. Use a unique identifier instead.
- Custom identifiers do not need to be declared in YAML/JSON.
- `WithPermission()` searches only the all-decisions catalog; an agent-declared identifier missing there fails the Build with `KeyNotFoundException`.
- First non-null decision wins; earlier session entries take precedence.
- Session decisions are not written to the interaction directory and have no JSON snapshot.

---

#### 3.2.9 🛠️✅ Configure Agent Response Validators

Validators implement `IAiInteractionResultValidator` and run after each attempt.

```csharp
public class AgentResponseValidator : IAiInteractionResultValidator
{
    public IValidate Validate(IAiInteractionDataBank? bank = null)
    {
        // Check file exists, JSON shape, content match
        if (/* all good */) return ValidationModel.Success();
        return ValidationModel.Failure("Expected file X with JSON { content: ... }");
    }
}
```

Usage:

```csharp
.WithResponseValidator(new AgentResponseValidator())
.WithMaxAttempts(3)
```

**Retry flow:**

```text
Attempt 1
  → Agent streams response
  → Validator checks
  → ❌ Failed
        ↓
Attempt 2  (prompt includes previous error)
  → Agent streams response
  → Validator checks
  → ✅ Passed
```

Example failure messages:

| Scenario | Message |
|----------|---------|
| Output file not found | `Output file '<path>' does not exist.` |
| JSON parse failed | `Response is not valid JSON: ...` |
| Missing field | `Expected field 'content' is missing or empty.` |
| Content mismatch | `File content does not match expected value.` |

---

#### 3.2.10 🛠️🔍 Configure Logging and Monitoring

Event handlers live in:

- `{RepoRoot}/DevQAProdCom.NET.AI.GitHubCopilot/Handlers/`
- `{RepoRoot}/DevQAProdCom.NET.AI.MicrosoftAgentFramework/Handlers/`

Current handler: `AssistantMessageEventHandler`.

MAF wraps it with `GitHubCopilotSessionEventAiContentHandler`, which adapts `AIContent` from `Microsoft.Extensions.AI` to the shared `IAiInteractionHandler` interface. This makes it provider-agnostic.

```csharp
public class GitHubCopilotAssistantMessageEventAiContentHandler
    : GitHubCopilotSessionEventAiContentHandler(
        new AssistantMessageEventHandler(logger), logger)
```

Add it:

```csharp
.WithDefaultContentHandlers()
// or
.WithAiContentHandlers(new GitHubCopilotAssistantMessageEventAiContentHandler(logger))
```

The GitHub Copilot SDK exposes many session events. A few examples:

| Event type | What it tells you |
|------------|-------------------|
| `assistant.message` | Final assistant text, reasoning, tool requests |
| `assistant.reasoning` | Model reasoning trace |
| `permission.requested` | User/tool permission request |
| `permission.completed` | Permission decision result |
| `tool.execution_start` | Tool call started |
| `tool.execution_complete` | Tool call finished |
| `subagent.started` / `subagent.completed` | Subagent lifecycle |
| `session.error` | Session-level error |
| `mcp.tools.list_changed` | MCP server tool list changed |

**Logged output examples:**

```text
🤖🧠 AI REASONINGS:
I will read the file and then write the output.

🤖📝 AI TEXT CONTENT:
The file has been written successfully.

Permission Request:
Type= GitHub.Copilot.PermissionRequest
Body = { "tool": "view", "path": "<file-path>" }
```

Permission requests are logged via `ConfigureOnPermissionRequest` in `SessionConfigBuilder`. You can also redirect them to a file:

```csharp
.WithPermissionRequestsLogFile("<path>/permissions.log")
```

#### 3.2.11 ⚡ Invoke Agents

##### 3.2.11.1 Test Explorer vs Console App

| Goal | Configuration | Notes |
|------|---------------|-------|
| Run NUnit tests in Test Explorer | `Debug` or `Release` | Standard test runner |
| Run as console app | `DebugAsApp` | Uses `launchSettings.json` |

Switching modes: clean + rebuild.

```bash
dotnet clean Tests.DevQAProdCom.NET.AI
dotnet build Tests.DevQAProdCom.NET.AI --configuration DebugAsApp --force
```

##### 3.2.11.2 GitHub Copilot → MAF (Microsoft Agent Framework) `AIAgent`

`GitHubCopilotAiAgentInteractor.GetAiAgentAsync()`:

```text
1. Set up/provide/use default temp interaction configuration directories (working directory/base directory/directory for interaction configuration data)
        ↓
2. Start the provider-specific client, for instance CopilotClient
        ↓
3. Build Session, for instance, SessionConfigBuilder.Build():
        - Resolve agents + configurations
        - Configure model, tools, instructions, skills, MCP servers
        - Configure permissions, hooks
        - Save relevant files to temp .github/
        - Substitute `.github` directory if file-based hooks exist
        ↓
4. Get the initialized session that references `Directory For Interaction Configuration Data` and contains only the agents, instructions, etc. related to the interaction.
        ↓
5. Transform the provider-specific session config into a Microsoft Agent Framework `AIAgent`: `copilotClient.AsAIAgent(sessionConfig, ownsClient: true)`
```

##### 3.2.11.3 MAF (Microsoft Agent Framework) Invocation Flow

`MicrosoftAiAgentInteractor.InvokeAiAgentWithStreamingAsync()`:

```text
Create session (or reuse)
        ↓
For attempt = 1 .. maxAttempts:
        ├─► Stream AgentResponseUpdate.Contents
        │     ↓
        │   For each AIContent:
        │     → pass to every IAiContentHandler
        ├─► Call handler.Finally()
        ├─► Run responseValidationFunc
        │     ↓
        ├─► ✅ Passed  → break
        └─► ❌ Failed  → append error to next prompt
        ↓
Return IAiInteractionDataBank
```

Default interaction timeout: **15 minutes**.

---


## 📎 LINKS

- [Microsoft Agent Framework](https://github.com/microsoft/agent-framework)
- [GitHub Copilot SDK](https://github.com/github/copilot-sdk)
- [GitHub Copilot CLI documentation](https://docs.github.com/en/copilot)
