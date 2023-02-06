FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

WORKDIR /src
COPY *.sln ./
COPY */*.csproj ./
RUN find . -name '*.csproj' -exec bash -c 'mkdir -p ${0%.*}/ && mv $0 ${0%.*}/' {} \;

WORKDIR /src/DeliverySoft.DomainServiceEmployees.WebApi
RUN dotnet restore "DeliverySoft.DomainServiceEmployees.WebApi.csproj"

WORKDIR /src
COPY . .

WORKDIR /src/DeliverySoft.DomainServiceEmployees.WebApi
RUN dotnet publish "DeliverySoft.DomainServiceEmployees.WebApi.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "DeliverySoft.DomainServiceEmployees.WebApi.dll"]
