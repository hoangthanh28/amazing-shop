FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Declative the environment variables
ENV SOURCE_API=gw

# Copy everything else and build
#RUN dotnet restore ./$SOURCE_API /property:Configuration=Release
COPY src/gw/src ./gw

RUN dotnet publish ./$SOURCE_API -c Release -o /app/out
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11
WORKDIR /app
COPY --from=build-env /app/out .
RUN apk add icu-libs
# https://www.abhith.net/blog/docker-sql-error-on-aspnet-core-alpine/
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENTRYPOINT ["dotnet", "AmazingShop.Gateway.dll"]