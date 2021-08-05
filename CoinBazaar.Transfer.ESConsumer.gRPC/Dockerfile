#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CoinBazaar.Transfer.ESConsumer.gRPC/CoinBazaar.Transfer.ESConsumer.gRPC.csproj", "CoinBazaar.Transfer.ESConsumer.gRPC/"]
COPY ["CoinBazaar.Infrastructure/CoinBazaar.Infrastructure.csproj", "CoinBazaar.Infrastructure/"]
RUN dotnet restore "CoinBazaar.Transfer.ESConsumer.gRPC/CoinBazaar.Transfer.ESConsumer.gRPC.csproj"
COPY . .
WORKDIR "/src/CoinBazaar.Transfer.ESConsumer.gRPC"
RUN dotnet build "CoinBazaar.Transfer.ESConsumer.gRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoinBazaar.Transfer.ESConsumer.gRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoinBazaar.Transfer.ESConsumer.gRPC.dll"]
