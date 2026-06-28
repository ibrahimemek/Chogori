FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["shared/SharedKernel/SharedKernel.csproj", "shared/SharedKernel/"]
COPY ["services/order/src/Order.Domain/Order.Domain.csproj", "services/order/src/Order.Domain/"]
COPY ["services/order/src/Order.Application/Order.Application.csproj", "services/order/src/Order.Application/"]
COPY ["services/order/src/Order.Infrastructure/Order.Infrastructure.csproj", "services/order/src/Order.Infrastructure/"]
COPY ["services/order/src/Order.API/Order.API.csproj", "services/order/src/Order.API/"]

RUN dotnet restore "services/order/src/Order.API/Order.API.csproj"

COPY shared/SharedKernel/ shared/SharedKernel/
COPY services/order/ services/order/

WORKDIR "/src/services/order/src/Order.API"
RUN dotnet build "Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]