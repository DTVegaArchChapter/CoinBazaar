#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CoinBazaar.Wallet.API/CoinBazaar.Wallet.API.csproj", "CoinBazaar.Wallet.API/"]
RUN dotnet restore "CoinBazaar.Wallet.API/CoinBazaar.Wallet.API.csproj"
COPY . .
WORKDIR "/src/CoinBazaar.Wallet.API"
RUN dotnet build "CoinBazaar.Wallet.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoinBazaar.Wallet.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoinBazaar.Wallet.API.dll"]
