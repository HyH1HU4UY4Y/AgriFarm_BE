#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Service.FarmPesticide/Service.Pesticide.csproj", "src/Services/Service.FarmPesticide/"]
COPY ["src/Core/Common/SharedApplication.csproj", "src/Core/Common/"]
COPY ["src/Core/Domain/SharedDomain.csproj", "src/Core/Domain/"]
COPY ["src/Core/EventBus/EventBus.csproj", "src/Core/EventBus/"]
COPY ["src/Infrastructure/Infrastructure.FarmPesticide/Infrastructure.Pesticide.csproj", "src/Infrastructure/Infrastructure.FarmPesticide/"]
RUN dotnet restore "./src/Services/Service.FarmPesticide/./Service.Pesticide.csproj"
COPY . .
WORKDIR "/src/src/Services/Service.FarmPesticide"
RUN dotnet build "./Service.Pesticide.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Service.Pesticide.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.Pesticide.dll"]