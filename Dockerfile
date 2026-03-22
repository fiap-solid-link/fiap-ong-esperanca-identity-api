# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar solution e csprojs para cache de restore
COPY Esperanca.Identity.sln ./
COPY src/Esperanca.Identity.Domain/Esperanca.Identity.Domain.csproj src/Esperanca.Identity.Domain/
COPY src/Esperanca.Identity.Application/Esperanca.Identity.Application.csproj src/Esperanca.Identity.Application/
COPY src/Esperanca.Identity.Infrastructure/Esperanca.Identity.Infrastructure.csproj src/Esperanca.Identity.Infrastructure/
COPY src/Esperanca.Identity.WebApi/Esperanca.Identity.WebApi.csproj src/Esperanca.Identity.WebApi/
COPY tests/Esperanca.Identity.UnitTests/Esperanca.Identity.UnitTests.csproj tests/Esperanca.Identity.UnitTests/
COPY tests/Esperanca.Identity.IntegrationTests/Esperanca.Identity.IntegrationTests.csproj tests/Esperanca.Identity.IntegrationTests/

RUN dotnet restore

# Copiar todo o codigo e publicar
COPY . .
RUN dotnet publish src/Esperanca.Identity.WebApi/Esperanca.Identity.WebApi.csproj -c Release -o /app/publish --no-restore

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Esperanca.Identity.WebApi.dll"]
