FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["shared/SharedKernel/SharedKernel.csproj", "shared/SharedKernel/"]
COPY ["services/payment/src/Payment.Domain/Payment.Domain.csproj", "services/payment/src/Payment.Domain/"]
COPY ["services/payment/src/Payment.Application/Payment.Application.csproj", "services/payment/src/Payment.Application/"]
COPY ["services/payment/src/Payment.Infrastructure/Payment.Infrastructure.csproj", "services/payment/src/Payment.Infrastructure/"]
COPY ["services/payment/src/Payment.API/Payment.API.csproj", "services/payment/src/Payment.API/"]

RUN dotnet restore "services/payment/src/Payment.API/Payment.API.csproj"

COPY shared/SharedKernel/ shared/SharedKernel/
COPY services/payment/ services/payment/

WORKDIR "/src/services/payment/src/Payment.API"
RUN dotnet build "Payment.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Payment.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Payment.API.dll"]