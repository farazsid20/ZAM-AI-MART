# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files and restore (layer-cached)
COPY src/ZamAiMart.Core/ZamAiMart.Core.csproj           src/ZamAiMart.Core/
COPY src/ZamAiMart.Infrastructure/ZamAiMart.Infrastructure.csproj  src/ZamAiMart.Infrastructure/
COPY src/ZamAiMart.API/ZamAiMart.API.csproj             src/ZamAiMart.API/
RUN dotnet restore src/ZamAiMart.API/ZamAiMart.API.csproj

# Copy full source and publish
COPY src/ src/
RUN dotnet publish src/ZamAiMart.API/ZamAiMart.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Non-root user for security
RUN adduser --disabled-password --gecos "" appuser \
    && mkdir -p /app/data \
    && chown -R appuser /app
USER appuser

COPY --from=build /app/publish .

# Persist SQLite database
VOLUME ["/app/data"]

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=30s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "ZamAiMart.API.dll"]
