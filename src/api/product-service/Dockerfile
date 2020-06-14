FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Declative the environment variables
ENV SOURCE_API=src/AmazingShop.Product.Api
ENV SOURCE_APPLICATION=src/AmazingShop.Product.Application
ENV SOURCE_DOMAIN=src/AmazingShop.Product.Domain
ENV SOURCE_PERSISTENCE=src/AmazingShop.Product.Persistence
ENV SOURCE_INFRASTRUCTURE=src/AmazingShop.Product.Infrastructure

# Copy csproj and restore as distinct layers
COPY $SOURCE_API/*.csproj ./$SOURCE_API/
COPY $SOURCE_APPLICATION/*.csproj ./$SOURCE_APPLICATION/
COPY $SOURCE_DOMAIN/*.csproj ./$SOURCE_DOMAIN/
COPY $SOURCE_PERSISTENCE/*.csproj ./$SOURCE_PERSISTENCE/
COPY $SOURCE_INFRASTRUCTURE/*.csproj ./$SOURCE_INFRASTRUCTURE/

# Copy everything else and build
# RUN dotnet restore ./$SOURCE_API /property:Configuration=Release
COPY src/. ./src/

RUN dotnet publish ./$SOURCE_API -c Release -o /app/out
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "AmazingShop.Product.Api.dll"]