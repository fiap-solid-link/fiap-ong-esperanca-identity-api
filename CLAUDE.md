# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build, Test & Run

```bash
dotnet build                                          # Build all projects
dotnet test                                           # Run all tests
dotnet test tests/Esperanca.Identity.UnitTests        # Unit tests only
dotnet run --project src/Esperanca.Identity.WebApi     # Run locally
docker compose up -d                                  # API + PostgreSQL (localhost:5010)
```

EF Core migrations (Infrastructure is the migrations project, WebApi is the startup):
```bash
dotnet ef migrations add <Name> --project src/Esperanca.Identity.Infrastructure --startup-project src/Esperanca.Identity.WebApi
dotnet ef database update --project src/Esperanca.Identity.Infrastructure --startup-project src/Esperanca.Identity.WebApi
```

## Architecture

Clean Architecture + Vertical Slice. Folders represent **business concepts** (e.g. `Autenticacao/`, `Usuarios/`), never technical concepts (no `Entities/`, `Repositories/`, `Controllers/` at the first level). Cross-cutting concerns go in `_Shared/`.

**Dependency flow:**
```
WebApi → Application → Domain (zero external deps)
             ↑
       Infrastructure (references Application, not Domain directly)
```

**Dependency inversion:** Infrastructure implements interfaces defined in Application (`IAppDbContext`, `IJwtService`, `IPasswordHasher`, `IAppLocalizer`) and Domain (`IUsuarioRepository`, `IRoleRepository`, `IRefreshTokenRepository`).

## Module Pattern

Each layer has a static Module class that registers its own dependencies. No `DependencyInjection.cs` extension methods — `Program.cs` stays minimal:

```
IdentityWebApiModule → IdentityApplicationModule + IdentityInfrastructureModule
```

New services must be registered in the appropriate Module, never in `Program.cs`.

## Vertical Slice Convention

Each use case is a self-contained folder with Command/Query + Handler + Validator:

```
Application/Autenticacao/Login/
├── LoginCommand.cs      # record implementing IRequest<Result<T>>
├── LoginHandler.cs      # sealed class, primary constructor injection
└── LoginValidator.cs    # AbstractValidator<T>, receives IAppLocalizer
```

Handlers use `IAppDbContext` for persistence (not `IUnitOfWork`). Controllers are thin — receive request, `mediator.Send()`, return result.

## Result Pattern

All handlers return `Result<T>` — never throw HTTP exceptions:

```csharp
Result<T>.Ok(data)           // 200
Result<T>.Created(data)      // 201
Result<T>.Fail(error)        // 400
Result<T>.NotFound(error)    // 404
Result<T>.Unauthorized(error) // 401
```

## Internationalization (i18n)

All user-facing messages use `IAppLocalizer` + `IdentityErrorCodes` constants. Never hardcode strings:

```csharp
localizer[IdentityErrorCodes.EmailJaCadastrado]  // resolves from pt-BR.json or en.json
```

Error codes follow pattern `Identity:NNN`. JSON files in `Application/_Shared/Localization/` are embedded resources. Add new language by creating a JSON file and marking it as `EmbeddedResource` in the Application `.csproj`.

## Centralized Package Management

`Directory.Build.props` defines shared properties (TargetFramework, Nullable, etc.). `Directory.Packages.props` defines all package versions. Individual `.csproj` files reference packages **without versions**.

## Key Conventions

- **Domain has zero NuGet dependencies** — only pure C# entities, enums, and repository interfaces
- **Handlers are `sealed` classes** with primary constructor injection
- **All async methods accept `CancellationToken ct`**
- **Validators inject `IAppLocalizer`** via constructor for localized messages
- **`IUsuarioAutenticado`** (Application interface, implemented in WebApi) abstracts the authenticated user — handlers never read `HttpContext` directly
- **Database seed** runs automatically on startup via `DatabaseSeed.SeedAdminAsync()` — creates Admin user (`admin@esperanca.org` / `Admin@123`) and 3 roles (Admin, GestorONG, Doador)
- **JWT:** HMAC-SHA256, access token 30min, refresh token 7 days. Claims: `sub`, `email`, `roles`
