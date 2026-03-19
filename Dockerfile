# Multi-stage build for ASP.NET Core MVC app on Render

# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies first
COPY EventManagementApp.csproj ./
RUN dotnet restore

# Copy the rest of the application
COPY . ./

# Publish the app
RUN dotnet publish EventManagementApp.csproj -c Release -o /app/publish

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish ./

# Render expects the service to listen on its assigned port
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "EventManagementApp.dll"]