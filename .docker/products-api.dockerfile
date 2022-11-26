FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /zenithar
COPY . .
RUN dotnet restore

FROM build AS publish
RUN dotnet publish src/Zenithar.ProductsAPI/Zenithar.ProductsAPI.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Zenithar.ProductsAPI.dll"]
