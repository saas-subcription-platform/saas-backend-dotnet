# ---------- Build Stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore Timesheets/Timesheets.csproj

RUN dotnet publish Timesheets/Timesheets.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ---------- Runtime Stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5114

EXPOSE 5114

ENTRYPOINT ["dotnet","Timesheets.dll"]