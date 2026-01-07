# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG GITHUB_TOKEN

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["NuGet.config", "."]
COPY ["src/NotificationGateway.Presentation/NotificationGateway.Presentation.csproj", "src/NotificationGateway.Presentation/"]
COPY ["src/NotificationGateway.Application/NotificationGateway.Application.csproj", "src/NotificationGateway.Application/"]
COPY ["src/NotificationGateway.Contracts/NotificationGateway.Contracts.csproj", "src/NotificationGateway.Contracts/"]
COPY ["src/NotificationGateway.Domain/NotificationGateway.Domain.csproj", "src/NotificationGateway.Domain/"]
COPY ["src/NotificationGateway.Infrastructure/NotificationGateway.Infrastructure.csproj", "src/NotificationGateway.Infrastructure/"]
RUN dotnet restore "./src/NotificationGateway.Presentation/NotificationGateway.Presentation.csproj" --configfile NuGet.config
COPY . .
WORKDIR "/src/src/NotificationGateway.Presentation"
RUN dotnet build "./NotificationGateway.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotificationGateway.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationGateway.Presentation.dll"]