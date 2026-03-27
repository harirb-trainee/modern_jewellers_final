# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution and projects
COPY ["ModernJwellers.sln", "./"]
COPY ["MJ/MJ.csproj", "MJ/"]
COPY ["MJ.Application/MJ.Application.csproj", "MJ.Application/"]
COPY ["MJ.Domain/MJ.Domain.csproj", "MJ.Domain/"]
COPY ["MJ.Infrastructure/MJ.Infrastructure.csproj", "MJ.Infrastructure/"]

# Restore dependencies
RUN dotnet restore

# Copy all files
COPY . .
WORKDIR /src/MJ
RUN dotnet build "MJ.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "MJ.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Stage (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Port for App Runner
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MJ.dll"]
