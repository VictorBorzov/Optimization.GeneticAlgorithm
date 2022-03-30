#ARG BUILD_TAG=6.0-alpine3.14
#ARG BASE_TAG=6.0-alpine3.14
#ARG BUILD_TAG=6.0-alpine
#ARG BASE_TAG=6.0-alpine
ARG BUILD_TAG=6.0
ARG BASE_TAG=6.0
FROM mcr.microsoft.com/dotnet/aspnet:${BASE_TAG} AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:${BUILD_TAG} AS build
WORKDIR /src
COPY ["WDS.Gateway.Web/WDS.Gateway.Web.csproj", "WDS.Gateway.Web/"]
RUN dotnet restore "WDS.Gateway.Web/WDS.Gateway.Web.csproj"
COPY . .
WORKDIR "/src/WDS.Gateway.Web"
RUN dotnet build "WDS.Gateway.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WDS.Gateway.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WDS.Gateway.Web.dll"]
