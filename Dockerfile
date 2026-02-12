# --- Base image ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# --- Build image ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем весь проект сразу, включая wwwroot
COPY . .

# Восстанавливаем зависимости
RUN dotnet restore "MusicApp.csproj"

# Сборка проекта
RUN dotnet build "MusicApp.csproj" -c Release -o /app/build

# Публикация проекта
FROM build AS publish
RUN dotnet publish "MusicApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Final image ---
FROM base AS final
WORKDIR /app

# Копируем всё опубликованное
COPY --from=publish /app/publish .

# Запуск приложения
ENTRYPOINT ["dotnet", "MusicApp.dll"]
