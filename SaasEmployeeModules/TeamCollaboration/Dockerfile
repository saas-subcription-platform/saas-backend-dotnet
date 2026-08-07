# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore TeamCollaboration/TeamCollaboration.csproj

RUN dotnet publish TeamCollaboration/TeamCollaboration.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5034

EXPOSE 5034

ENTRYPOINT ["dotnet","TeamCollaboration.dll"]