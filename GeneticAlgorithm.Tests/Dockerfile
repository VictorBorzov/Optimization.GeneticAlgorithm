ARG BUILD_TAG=6.0
ARG BASE_TAG=6.0
FROM mcr.microsoft.com/dotnet/runtime:${BASE_TAG} AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:${BASE_TAG} AS build
WORKDIR /src
COPY ["GeneticAlgorithm.Tests/GeneticAlgorithm.Tests.csproj", "GeneticAlgorithm.Tests/"]
COPY ["GeneticAlgorithm/GeneticAlgorithm.csproj", "GeneticAlgorithm/"]
RUN dotnet restore "GeneticAlgorithm.Tests/GeneticAlgorithm.Tests.csproj"
COPY . .
WORKDIR "/src/GeneticAlgorithm.Tests"
RUN dotnet build "GeneticAlgorithm.Tests.csproj" -c Release -o /app/build
ENTRYPOINT ["dotnet", "test"]