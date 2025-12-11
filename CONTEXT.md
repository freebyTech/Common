# CONTEXT.md - Project Memory for freebyTech.Common

## Project Overview
**freebyTech.Common** is a shared .NET 10.0 library created by freebyTech LLC (James Eby) that provides common utilities, extensions, and infrastructure code used across multiple freebyTech software projects. The library is cross-platform compatible (Windows, Linux, macOS) and published to NuGet.

## Repository Information
- **Current Branch**: develop (also the main branch)
- **Technology Stack**: .NET 10.0
- **NuGet Package**: freebyTech.Common
- **License**: See LICENSE file
- **Copyright**: © 2023 freebyTech LLC

## Project Structure
```
D:\dev\Common/
├── src/
│   ├── freebyTech.Common/              # Main library project
│   └── freebyTech.Common.Tests/        # Unit tests
├── .vscode/                            # VS Code settings
├── README.md                           # Build and usage instructions
├── CONTEXT.md                          # This file - Claude's project memory
├── Jenkinsfile                         # CI/CD pipeline
├── .csharpierrc.json                   # C# formatter config
├── .editorconfig                       # Editor configuration
└── sc.sh                               # Script file (untracked)
```

## Key Library Components

### 1. Command Line Processing
- `CommandLine/CommandCollection.cs` - Command collection management
- `CommandLine/CommandLineArgument.cs` - Command argument parsing
- `Configuration/ProperCommandLineConfigurationProvider.cs` - Enhanced command line configuration
- `ExtensionMethods/CommandLineExtensions.cs` - Command line utilities

### 2. Data Access & Repositories
- `Data/GenericRepository.cs` - Generic repository pattern
- `Data/GenericReadonlyRepository.cs` - Read-only repository
- `Data/Interfaces/` - Repository and model interfaces:
  - `IEditableModel.cs`, `IEditableResource.cs`, `ICompactEditableResource.cs`
  - `IFindableById.cs`, `IFindableByGuid.cs`
- `Data/EditableModelConfiguration.cs` - Configuration for editable models

### 3. Entities & Resources
- **Entities** (Database models):
  - `BaseEntity.cs` - Base entity with active/inactive tracking
  - `BaseEntityNonActive.cs` - Base entity without active tracking
  - `AppSettingEntity.cs` - Application settings entity

- **Resources** (DTOs/View Models):
  - `BaseResource.cs`, `BaseResourceNonActive.cs` - Full resource views
  - `BaseCompactResourceView.cs`, `BaseCompactResourceViewNonActive.cs` - Compact views
  - `BaseSimpleResourceView.cs`, `BaseSimpleResourceViewNonActive.cs` - Simple views
  - `LookupBaseResource.cs`, `LookupBaseResourceNonActive.cs` - Lookup resources
  - `AppSettingResource.cs` - Application settings resource

### 4. Extension Methods
Extensive collection of extension methods in `ExtensionMethods/`:
- `AutoMapperExtensions.cs` - AutoMapper utilities
- `CharExtensions.cs` - Character operations
- `CollectionExtensions.cs` - Collection utilities
- `ConfigurationExtensions.cs` - Configuration helpers
- `DateTimeExtensions.cs` - DateTime utilities
- `EnvironmentExtensions.cs` - Environment helpers
- `InflectorExtensions.cs` - Pluralization/singularization
- `MethodInfoExtensions.cs` - Reflection helpers
- `ModelExtensions.cs` - Model utilities
- `ServiceRegistrationExtensions.cs` - DI registration
- `StopWatchExtensions.cs` - Timing utilities
- `StringExtensions.cs` - String operations
- `UriExtensions.cs` - URI manipulation

### 5. Logging Infrastructure
Comprehensive logging system in `Logging/`:
- **Core**:
  - `LoggerBase.cs` - Base logger implementation
  - `GenericLogLevel.cs` - Log level abstraction
  - `GenericLogEventInfo.cs` - Log event information
  - `LoggerDuration.cs` - Duration tracking
  - `PushLogItem.cs` - Log item pushing
  - `StaticApplicationLoggingMetrics.cs` - Application metrics

- **Framework Adapters**:
  - `NLogLogFrameworkAgent.cs` - NLog integration
  - `SerilogFrameworkAgent.cs` - Serilog integration

- **Logger Types**:
  - `BasicLogger.cs` - Basic logging (IBasicLogger)
  - `BasicInstrumentationLogger.cs` - Instrumentation (IInstrumentationLogger)
  - `BasicRunMetricsLogger.cs` - Runtime metrics (IRunMetricsLogger)
  - `BasicValidationLogger.cs` - Validation logging (IValidationLogger)

### 6. Messaging
- **Email**:
  - `Messaging/Mail/SmtpMailProvider.cs` - SMTP provider
  - `Messaging/Mail/SmtpMailService.cs` - Mail service
  - `Options/SmtpMailOptions.cs` - SMTP configuration
  - `Interfaces/IMailProvider.cs` - Mail provider interface

- **Slack**:
  - `Messaging/Slack/SlackMessenger.cs` - Slack integration
  - `Messaging/Slack/Model/` - Slack message models (Attachment, Fields, SlackMessage, etc.)

### 7. Process Execution
- `Process/CommandLineExecutionProvider.cs` - Execute command line processes
- `Process/CommandExecutionParamModel.cs` - Execution parameters

### 8. Services
- `Service/BaseClasses/ServiceBase.cs` - Base service class
- `Service/BaseClasses/ServiceFactoryBase.cs` - Service factory pattern
- `Service/BaseClasses/BaseCommandArguments.cs` - Command arguments base
- `Service/Interfaces/IService.cs`, `IServiceFactory.cs`

### 9. Environment Management
- `Environment/EnvironmentManager.cs` - Environment detection and management
- `Environment/ExecutionEnvironment.cs` - Execution context
- `Interfaces/IEnvironmentManager.cs`, `IExecutionEnvironment.cs`

### 10. Utilities
- `Helpers/Retry.cs` - Retry logic
- `Threading/ThreadsafeRandom.cs` - Thread-safe random number generation
- `Constants/AppSettingsConstants.cs` - Application constants
- `Constants/RegularExpressions.cs` - Regex patterns

## Dependencies (NuGet Packages)
- **AutoMapper** 13.0.1 - Object-to-object mapping
- **Microsoft.EntityFrameworkCore** 9.0.0 - ORM framework
- **Microsoft.Extensions.*** 9.0.0 - Configuration, DI, Logging
- **Serilog** 4.1.0 - Structured logging
- **NLog** 6.0.0 - Logging framework
- **DasMulli.Win32.ServiceUtils** 2.1.0 - Windows service utilities
- **System.Configuration.ConfigurationManager** 9.0.0 - Configuration management

## Build & Test Commands
```bash
# Build the library
cd ./src/freebyTech.Common
dotnet build

# Run unit tests
cd ./src/freebyTech.Common.Tests
dotnet test

# Docker build
docker build ./src -t freebyTech/common
```

## Testing Structure
Unit tests in `freebyTech.Common.Tests/`:
- `Configuration/ProperCommandLineConfigurationTests.cs`
- `ExtensionMethods/CharExtensionsTests.cs`
- `ExtensionMethods/InflectorExtensionsTests.cs`
- `ExtensionMethods/StringExtensionsTests.cs`
- `ExtensionMethods/UriExtensionTests.cs`
- `Process/CommandLineExecutionProviderTest.cs`

## Recent Activity (from git log)
Latest commits:
- Merged release/beta branch
- Updated README.md
- Added back Simple Resource View types
- Fixed inheritance issues
- Released new types after testing

## Design Patterns & Architecture
1. **Repository Pattern** - Generic repositories for data access
2. **Factory Pattern** - Service factories for service creation
3. **Extension Method Pattern** - Fluent APIs and utility extensions
4. **Adapter Pattern** - Logging framework adapters (NLog, Serilog)
5. **Strategy Pattern** - Multiple logger types for different concerns
6. **Base Class Inheritance** - Entities and Resources use base classes for common functionality

## Key Features
- **Cross-platform** .NET 10.0 library
- **Comprehensive logging** with multiple framework support
- **Generic repository pattern** for Entity Framework
- **Rich extension methods** for common .NET types
- **Command line processing** with enhanced configuration
- **Messaging support** for Email (SMTP) and Slack
- **Service abstraction** for building background services
- **Thread-safe utilities** and helpers
- **ImplicitUsings** enabled for cleaner code

## Configuration
- Uses .editorconfig for consistent code style
- CSharpier for code formatting (.csharpierrc.json)
- Nullable reference types enabled
- Versioning controlled by BUILD_VERSION environment variable

## Notes for Development
- Main branch is "develop"
- Package name changes based on PACKAGE_ID env var (defaults to freebyTech.Common.Local for local builds)
- Version defaults to 0.1.0.0 unless BUILD_VERSION is set
- Tests use standard .NET testing frameworks

## Recent Upgrades
- Upgraded from .NET 6.0 to .NET 10.0 (December 2025)
- All NuGet packages updated to latest stable versions compatible with .NET 10
- NLog upgraded from 4.5.11 to 6.0.0 (major version upgrade - review breaking changes)
- Entity Framework Core upgraded from 7.0.2 to 9.0.0
- AutoMapper upgraded from 12.0.1 to 13.0.1
- Microsoft.Extensions packages upgraded from 7.0.0 to 9.0.0
- Enabled ImplicitUsings in main project for consistency
- Removed outdated .NET Framework 4.5 conditional compilation directives

---
**Last Updated**: 2025-12-10 (Upgraded to .NET 10.0)
**Maintained by**: Claude Code Assistant
