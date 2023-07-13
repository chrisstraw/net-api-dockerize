#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Core/Example.Container.Core.Common/Example.Container.Core.Common.csproj", "src/Core/Example.Container.Core.Common/"]
COPY ["src/Core/Example.Container.Core.Config/Example.Container.Core.Config.csproj", "src/Core/Example.Container.Core.Config/"]
COPY ["src/Core/Example.Container.Core.Domain/Example.Container.Core.Domain.csproj", "src/Core/Example.Container.Core.Domain/"]
COPY ["src/Infrastructure/Example.Container.Infrastructure.Abstractions/Example.Container.Infrastructure.Abstractions.csproj", "src/Infrastructure/Example.Container.Infrastructure.Abstractions/"]
COPY ["src/Infrastructure/Example.Container.Infrastructure.Db/Example.Container.Infrastructure.Db.csproj", "src/Infrastructure/Example.Container.Infrastructure.Db/"]
COPY ["src/Infrastructure/Example.Container.Infrastructure.Repos/Example.Container.Infrastructure.Repos.csproj", "src/Infrastructure/Example.Container.Infrastructure.Repos/"]
COPY ["src/Host/Example.Container.Host.WebApi/Example.Container.Host.WebApi.csproj", "src/Host/Example.Container.Host.WebApi/"]
RUN dotnet restore "src/Host/Example.Container.Host.WebApi/Example.Container.Host.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Host/Example.Container.Host.WebApi"
RUN dotnet build "Example.Container.Host.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Example.Container.Host.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Container.Host.WebApi.dll"]