# Stage 1: Base image with .NET runtime and ICU support
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install ICU for full globalization support
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LC_ALL=en_US.UTF-8
ENV LANG=en_US.UTF-8

# Stage 2: Build stage with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the main API project file
COPY /InventoryApplication.Api/InventoryApplication.Api.csproj InventoryApplication.Api/

# Copy project dependencies (make sure paths are correct)
COPY /InventoryApplication.Domain/InventoryApplication.Domain.csproj InventoryApplication.Domain/
COPY /InventoryApplication.Infrastructure/InventoryApplication.Infrastructure.csproj InventoryApplication.Infrastructure/
COPY /InventoryApplication.Services/InventoryApplication.Services.csproj InventoryApplication.Services/

# Restore dependencies
RUN dotnet restore "InventoryApplication.Api/InventoryApplication.Api.csproj"

# Copy the entire solution for building
COPY . .
WORKDIR /src/InventoryApplication.Api
RUN dotnet build "InventoryApplication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 3: Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InventoryApplication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 4: Final stage - Runtime image with the published output
FROM base AS final
WORKDIR /app

# Copy the published application from the previous stage
COPY --from=publish /app/publish .

# Set environment variables for globalization in the final stage
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LC_ALL=en_US.UTF-8
ENV LANG=en_US.UTF-8

# Entry point for the application
ENTRYPOINT ["dotnet", "InventoryApplication.Api.dll"]
