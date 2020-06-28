FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
# Copy csproj and restore as distinct layers
COPY src .

# Copy everything else and build
RUN dotnet restore IdpServer.csproj

RUN dotnet publish IdpServer.csproj -c Release -o out
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3-alpine3.11
WORKDIR /app
COPY --from=build-env /app/out .
RUN apk add icu-libs
# https://www.abhith.net/blog/docker-sql-error-on-aspnet-core-alpine/
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENTRYPOINT ["dotnet", "IdpServer.dll"]