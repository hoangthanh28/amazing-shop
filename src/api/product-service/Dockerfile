FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Declative the environment variables
ENV SOURCE_API=api/product-service/src/AmazingShop.Product.Api

# Copy everything else and build
# RUN dotnet restore ./$SOURCE_API /property:Configuration=Release
COPY src/shared/. ./shared
COPY src/api/product-service/ ./api/product-service

RUN dotnet publish ./$SOURCE_API -c Release -o /app/out
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11
WORKDIR /app
COPY --from=build-env /app/out .
RUN apk add icu-libs
# https://www.abhith.net/blog/docker-sql-error-on-aspnet-core-alpine/
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENTRYPOINT ["dotnet", "AmazingShop.Product.Api.dll"]