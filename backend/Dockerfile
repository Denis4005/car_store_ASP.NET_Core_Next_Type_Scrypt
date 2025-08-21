# Используем официальный образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["backend.api/backend.api.csproj", "backend.api/"]
COPY ["backend.core/backend.core.csproj", "backend.core/"]
COPY ["backend.application/backend.application.csproj", "backend.application/"]
COPY ["backend.dataaccess/backend.dataaccess.csproj", "backend.dataaccess/"]
COPY ["backend.infrastructure/backend.infrastructure.csproj", "backend.infrastructure/"]
RUN dotnet restore "backend.api/backend.api.csproj"

COPY . .
WORKDIR "/src/backend.api"
RUN dotnet publish "backend.api.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "backend.api.dll"]