FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["shared/SharedKernel/SharedKernel.csproj", "shared/SharedKernel/"]
COPY ["services/shipping/src/Shipping.Domain/Shipping.Domain.csproj", "services/shipping/src/Shipping.Domain/"]
COPY ["services/shipping/src/Shipping.Application/Shipping.Application.csproj", "services/shipping/src/Shipping.Application/"]
COPY ["services/shipping/src/Shipping.Infrastructure/Shipping.Infrastructure.csproj", "services/shipping/src/Shipping.Infrastructure/"]
COPY ["services/shipping/src/Shipping.API/Shipping.API.csproj", "services/shipping/src/Shipping.API/"]

RUN dotnet restore "services/shipping/src/Shipping.API/Shipping.API.csproj"

COPY shared/SharedKernel/ shared/SharedKernel/
COPY services/shipping/ services/shipping/

WORKDIR "/src/services/shipping/src/Shipping.API"
RUN dotnet build "Shipping.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shipping.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shipping.API.dll"]