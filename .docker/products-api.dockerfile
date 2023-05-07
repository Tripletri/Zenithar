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
        --output-document /.postgresql/root.crt

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80

RUN mkdir -p /home/heim \
    && groupadd -r heim \
    && useradd -r -g heim -d /home/heim -s /sbin/nologin heim -u 300 \
    && chown -R heim:heim /home/heim

COPY --from=publish /.postgresql/root.crt /home/heim/.postgresql/root.crt
RUN chmod 0600 /home/heim/.postgresql/root.crt

ENTRYPOINT ["dotnet", "Zenithar.ProductsAPI.dll"]
