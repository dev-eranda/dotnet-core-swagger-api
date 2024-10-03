# Use official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT=Development

# Use SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Restore dependencies
WORKDIR /src
COPY ["api.csproj", "./"]
RUN dotnet restore "api.csproj"

# Build the app
COPY . .
WORKDIR "/src/"
RUN dotnet build -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage: copy the publish folder to the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]

