#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ApiGateways/YarpApiGw/YarpApiGw.csproj", "src/ApiGateways/YarpApiGw/"]
COPY ["src/BuildingBlocks/Common.Logging/BuildingBlocks.Logging.csproj", "src/BuildingBlocks/Common.Logging/"]
COPY ["src/BuildingBlocks/Shared/BuildingBlocks.Shared.csproj", "src/BuildingBlocks/Shared/"]
RUN dotnet restore "./src/ApiGateways/YarpApiGw/YarpApiGw.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/YarpApiGw"
RUN dotnet build "./YarpApiGw.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YarpApiGw.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YarpApiGw.dll"]