FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["shared/SharedKernel/SharedKernel.csproj", "shared/SharedKernel/"]
COPY ["services/catalog/src/Catalog.Domain/Catalog.Domain.csproj", "services/catalog/src/Catalog.Domain/"]
COPY ["services/catalog/src/Catalog.Application/Catalog.Application.csproj", "services/catalog/src/Catalog.Application/"]
COPY ["services/catalog/src/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "services/catalog/src/Catalog.Infrastructure/"]
COPY ["services/catalog/src/Catalog.API/Catalog.API.csproj", "services/catalog/src/Catalog.API/"]

RUN dotnet restore "services/catalog/src/Catalog.API/Catalog.API.csproj"

COPY shared/SharedKernel/ shared/SharedKernel/
COPY services/catalog/ services/catalog/

WORKDIR "/src/services/catalog/src/Catalog.API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]