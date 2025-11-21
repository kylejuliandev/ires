# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled-extra@sha256:64f42416803e32bee1f5d2d3eab5825581abd45b2e9c6f888fc873ff2c4cc378 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
LABEL org.opencontainers.image.source=https://github.com/kylejuliandev
LABEL org.opencontainers.image.description="Ires Frontend service which serves the web application."
LABEL org.opencontainers.image.licenses=AGPL-3.0

# This stage is used to build the service project
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-noble@sha256:c7445f141c04f1a6b454181bd098dcfa606c61ba0bd213d0a702489e5bd4cd71 AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
WORKDIR /src
COPY ["Ires.Frontend/Ires.Frontend.csproj", "Ires.Frontend/"]
COPY ["Ires.ServiceDefaults/Ires.ServiceDefaults.csproj", "Ires.ServiceDefaults/"]
RUN dotnet restore "./Ires.Frontend/Ires.Frontend.csproj"
COPY . .
WORKDIR "/src/Ires.Frontend"
RUN dotnet build "./Ires.Frontend.csproj" -c $BUILD_CONFIGURATION -a $TARGETARCH -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
RUN dotnet publish "./Ires.Frontend.csproj" -c $BUILD_CONFIGURATION -a $TARGETARCH -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ires.Frontend.dll"]