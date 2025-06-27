# Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8094
EXPOSE 8095

# Define com ambiente dev
ENV ASPNETCORE_ENVIRONMENT=Development

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos .csproj primeiro para aproveitar o cache de camadas do Docker
COPY ["Consumer/Consumer.csproj", "Consumer/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "Consumer/Consumer.csproj"

# Copia o restante dos arquivos
COPY . .

WORKDIR "/src/Consumer"
RUN dotnet publish -c Release -o /app/publish  

# Runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish . 
ENTRYPOINT ["dotnet", "Consumer.dll"]