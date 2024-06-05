FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN apt-get update && apt-get install -y chromium
ENV PUPPETER_EXECUTABLE_PATH=/usr/bin/chromium

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Manager.API/Manager.API.csproj", "Manager.API/"]
COPY ["Manager.Application/Manager.Application.csproj", "Manager.Application/"]
COPY ["Manager.Domain/Manager.Domain.csproj", "Manager.Domain/"]
COPY ["SharedKernel/SharedKernel.csproj", "SharedKernel/"]
COPY ["Manager.Infrastructure/Manager.Infrastructure.csproj", "Manager.Infrastructure/"]
RUN dotnet restore "Manager.API/Manager.API.csproj"
COPY . .
WORKDIR "/src/Manager.API"
RUN dotnet build "Manager.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Manager.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Manager.API.dll"]
