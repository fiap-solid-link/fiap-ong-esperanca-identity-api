# Esperança Identity API

API de **Identidade e Acesso** da plataforma **Conexão Solidária** — microsserviço responsável por autenticação, emissão de JWT e gerenciamento de perfis (RBAC).

**Bounded Context:** Identidade e Acesso

---

## Stack Tecnológica

| Componente | Tecnologia |
|------------|------------|
| Plataforma | .NET 10 / ASP.NET Core |
| ORM | Entity Framework Core 10 + Npgsql |
| Banco de Dados | PostgreSQL 16 |
| Mediator | MediatR 14 |
| Validação | FluentValidation 12 |
| Autenticação | JWT Bearer (HMAC-SHA256) |
| Hashing | BCrypt.Net-Next |
| Logging | Serilog |
| Documentação | Swagger / Swashbuckle |
| Containerização | Docker (multi-stage build) |
| Testes | xUnit |

---

## Arquitetura

**Clean Architecture** com **Vertical Slice** — pastas organizadas por conceito de negócio, não por conceito técnico.

```
WebApi → Application → Domain
             ↑
       Infrastructure
```

| Camada | Responsabilidade |
|--------|-----------------|
| **Domain** | Entidades, Value Objects, Enums, Interfaces de repositório. Zero dependências externas. |
| **Application** | Casos de uso (Command/Query + Handler + Validator), interfaces de serviço, internacionalização (i18n). Orquestradora de toda lógica de negócio. |
| **Infrastructure** | Implementações concretas: EF Core, Repositories, JWT Service, BCrypt. Referencia Application (inversão de dependência via `IAppDbContext`). |
| **WebApi** | Controllers finos (recebe request → envia pro MediatR → devolve resultado), Middleware. |

Cada camada possui um **Module** (`IdentityApplicationModule`, `IdentityInfrastructureModule`, `IdentityWebApiModule`) responsável por registrar suas próprias dependências. O `Program.cs` fica enxuto — apenas bootstrap e pipeline HTTP.

### Internacionalização (i18n)

Todas as mensagens de erro e validação são internacionalizadas via arquivos JSON embeddados:

- `Application/_Shared/Localization/pt-BR.json` — Português (padrão)
- `Application/_Shared/Localization/en.json` — Inglês

Códigos de erro tipados em `IdentityErrorCodes` (ex: `Identity:001`). Para adicionar um novo idioma, basta criar um novo JSON (ex: `es.json`) e incluí-lo como `EmbeddedResource` no `.csproj`.

---

## Endpoints

| Método | Rota | Acesso | Descrição |
|--------|------|--------|-----------|
| POST | `/api/auth/registrar` | Público | Registrar novo doador |
| POST | `/api/auth/login` | Público | Autenticar e obter JWT |
| POST | `/api/auth/refresh` | Autenticado | Renovar access token |
| GET | `/api/auth/me` | Autenticado | Dados do usuário logado |
| PUT | `/api/usuarios/perfil` | Autenticado | Atualizar perfil (nome, apelido) |
| POST | `/api/usuarios/{id}/conceder-gestor` | Admin | Conceder perfil GestorONG |
| DELETE | `/api/usuarios/{id}/revogar-gestor` | Admin | Revogar perfil GestorONG |
| GET | `/health` | Público | Health check (PostgreSQL) |

---

## Perfis e Permissões (RBAC)

| Recurso | Admin | GestorONG | Doador | Visitante |
|---------|-------|-----------|--------|-----------|
| Cadastrar usuário | — | — | — | Público |
| Autenticar | — | — | — | Público |
| Atualizar perfil | Sim | Sim | Sim | — |
| Conceder/Revogar GestorONG | Sim | — | — | — |

**JWT Claims:** `sub` (userId), `email`, `roles` (array: Admin, GestorONG, Doador)
**Access Token:** 30 min | **Refresh Token:** 7 dias

---

## Como Executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) e Docker Compose

### Com Docker Compose (recomendado)

```bash
docker compose up -d
```

- **API:** http://localhost:5010
- **Swagger:** http://localhost:5010/swagger
- **PostgreSQL:** localhost:5432 (user: `postgres`, password: `postgres`, db: `identity_db`)

### Localmente (sem Docker)

1. Subir o PostgreSQL (porta 5432, db `identity_db`)
2. Executar a API:

```bash
dotnet run --project src/Esperanca.Identity.WebApi
```

### Seed

Na primeira execução, o sistema cria automaticamente:
- 3 perfis: **Admin**, **GestorONG**, **Doador**
- Usuário admin: `admin@esperanca.org` / `Admin@123`

---

## Testes

```bash
dotnet test
```

---

## Banco de Dados

**PostgreSQL 16** — database `identity_db`

| Tabela | Descrição |
|--------|-----------|
| `usuarios` | Cadastro de usuários (nome, email, senha hash) |
| `perfis` | Roles do sistema (Admin, GestorONG, Doador) |
| `usuario_roles` | Relação N:N entre usuários e perfis |
| `refresh_tokens` | Tokens de refresh com expiração e revogação |

### Migrations

```bash
# Criar migration
dotnet ef migrations add NomeDaMigration --project src/Esperanca.Identity.Infrastructure --startup-project src/Esperanca.Identity.WebApi

# Aplicar migration
Basta executar a aplicação.
```
