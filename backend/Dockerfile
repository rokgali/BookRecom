FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish

WORKDIR /app

COPY . .

WORKDIR /app/API

RUN dotnet publish "backend.csproj" -c Release -o /app/API/build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migration

WORKDIR /app

COPY . .

WORKDIR /app/migration

RUN dotnet build -c Release -o /app/migration/build

FROM base AS final

WORKDIR /app/API
COPY --from=publish /app/API/build .

WORKDIR /app/migration
COPY --from=migration /app/migration/build .

WORKDIR /app/API
ENTRYPOINT ["dotnet", "backend.dll"]