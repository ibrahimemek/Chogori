FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["shared/SharedKernel/SharedKernel.csproj", "shared/SharedKernel/"]
COPY ["services/notification/src/Notification.Domain/Notification.Domain.csproj", "services/notification/src/Notification.Domain/"]
COPY ["services/notification/src/Notification.Application/Notification.Application.csproj", "services/notification/src/Notification.Application/"]
COPY ["services/notification/src/Notification.Infrastructure/Notification.Infrastructure.csproj", "services/notification/src/Notification.Infrastructure/"]
COPY ["services/notification/src/Notification.API/Notification.API.csproj", "services/notification/src/Notification.API/"]

RUN dotnet restore "services/notification/src/Notification.API/Notification.API.csproj"

COPY shared/SharedKernel/ shared/SharedKernel/
COPY services/notification/ services/notification/

WORKDIR "/src/services/notification/src/Notification.API"
RUN dotnet build "Notification.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.API.dll"]