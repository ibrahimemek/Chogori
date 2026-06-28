FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["shared/SharedKernel/SharedKernel.csproj", "shared/SharedKernel/"]
COPY ["services/inventory/src/Inventory.Domain/Inventory.Domain.csproj", "services/inventory/src/Inventory.Domain/"]
COPY ["services/inventory/src/Inventory.Application/Inventory.Application.csproj", "services/inventory/src/Inventory.Application/"]
COPY ["services/inventory/src/Inventory.Infrastructure/Inventory.Infrastructure.csproj", "services/inventory/src/Inventory.Infrastructure/"]
COPY ["services/inventory/src/Inventory.API/Inventory.API.csproj", "services/inventory/src/Inventory.API/"]

RUN dotnet restore "services/inventory/src/Inventory.API/Inventory.API.csproj"

COPY shared/SharedKernel/ shared/SharedKernel/
COPY services/inventory/ services/inventory/

WORKDIR "/src/services/inventory/src/Inventory.API"
RUN dotnet build "Inventory.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventory.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.API.dll"]