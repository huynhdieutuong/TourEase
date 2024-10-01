#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Notification/SignalR/SignalR.csproj", "src/Services/Notification/SignalR/"]
COPY ["src/BuildingBlocks/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj", "src/BuildingBlocks/BuildingBlocks.Messaging/"]
COPY ["src/BuildingBlocks/Contracts/BuildingBlocks.Contracts.csproj", "src/BuildingBlocks/Contracts/"]
COPY ["src/BuildingBlocks/Shared/BuildingBlocks.Shared.csproj", "src/BuildingBlocks/Shared/"]
COPY ["src/BuildingBlocks/Common.Logging/BuildingBlocks.Logging.csproj", "src/BuildingBlocks/Common.Logging/"]
RUN dotnet restore "./src/Services/Notification/SignalR/SignalR.csproj"
COPY . .
WORKDIR "/src/src/Services/Notification/SignalR"
RUN dotnet build "./SignalR.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SignalR.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalR.dll"]