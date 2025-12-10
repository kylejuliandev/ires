# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled-extra@sha256:9f1788f0dc1f3db11143a80b2b68b7612bf64bd23f367801bcbbf1861cce06c0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
LABEL org.opencontainers.image.source=https://github.com/kylejuliandev
LABEL org.opencontainers.image.description="Ires Frontend service which serves the web application."
LABEL org.opencontainers.image.licenses=AGPL-3.0

# This stage is used to build the service project
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-noble@sha256:d1823fecac3689a2eb959e02ee3bfe1c2142392808240039097ad70644566190 AS build
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
ARG VERSION
RUN dotnet publish "./Ires.Frontend.csproj" -c $BUILD_CONFIGURATION -a $TARGETARCH -o /app/publish -p:Version=$VERSION /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ires.Frontend.dll"]