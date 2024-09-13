#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Tour/Tour.API/Tour.API.csproj", "src/Services/Tour/Tour.API/"]
COPY ["src/BuildingBlocks/Common.Logging/BuildingBlocks.Logging.csproj", "src/BuildingBlocks/Common.Logging/"]
COPY ["src/Services/Tour/Tour.Application/Tour.Application.csproj", "src/Services/Tour/Tour.Application/"]
COPY ["src/BuildingBlocks/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "src/BuildingBlocks/BuildingBlocks.Messaging/"]
COPY ["src/BuildingBlocks/Contracts/BuildingBlocks.Contracts.csproj", "src/BuildingBlocks/Contracts/"]
COPY ["src/BuildingBlocks/Infrastructure/BuildingBlocks.Infrastructure.csproj", "src/BuildingBlocks/Infrastructure/"]
COPY ["src/BuildingBlocks/Shared/BuildingBlocks.Shared.csproj", "src/BuildingBlocks/Shared/"]
COPY ["src/Services/Tour/Tour.Domain/Tour.Domain.csproj", "src/Services/Tour/Tour.Domain/"]
COPY ["src/Services/Tour/Tour.Infrastructure.Redis/Tour.Infrastructure.Redis/Tour.Infrastructure.Redis.csproj", "src/Services/Tour/Tour.Infrastructure.Redis/Tour.Infrastructure.Redis/"]
COPY ["src/Services/Tour/Tour.Infrastructure/Tour.Infrastructure.csproj", "src/Services/Tour/Tour.Infrastructure/"]
RUN dotnet restore "./src/Services/Tour/Tour.API/Tour.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Tour/Tour.API"
RUN dotnet build "./Tour.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tour.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tour.API.dll"]