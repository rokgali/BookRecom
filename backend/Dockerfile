FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

EXPOSE 5103

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS debug

RUN apt-get update
RUN apt-get install -y unzip

RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /root/vsdbg

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

COPY --from=debug /root/vsdbg /root/vsdbg

WORKDIR /app/migration
COPY --from=migration /app/migration/build .

RUN chmod +x /app/

WORKDIR /app/API
ENTRYPOINT ["dotnet", "run"]