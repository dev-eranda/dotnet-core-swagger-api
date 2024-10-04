FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80

# Restore dependencies
COPY ["api.csproj", "./"]
RUN dotnet restore "api.csproj"

COPY . .
RUN dotnet build

ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:80"]
