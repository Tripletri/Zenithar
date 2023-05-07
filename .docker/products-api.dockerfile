FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /zenithar
COPY . .
RUN dotnet restore

FROM build AS publish
RUN dotnet publish src/Zenithar.ProductsAPI/Zenithar.ProductsAPI.csproj -c Release -o /app/publish

RUN mkdir --parents /.postgresql && \
    wget "https://storage.yandexcloud.net/cloud-certs/CA.pem" \
        --output-document /.postgresql/CA.pem

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80

COPY --from=publish /.postgresql/CA.pem .
RUN chmod 0600 CA.pem

ENTRYPOINT ["dotnet", "Zenithar.ProductsAPI.dll"]
