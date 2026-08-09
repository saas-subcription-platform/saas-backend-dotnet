# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore GoalandPerformance/GoalandPerformance.csproj

RUN dotnet publish GoalandPerformance/GoalandPerformance.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5072

ENTRYPOINT ["dotnet", "GoalandPerformance.dll"]