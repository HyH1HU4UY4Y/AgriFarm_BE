#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Service.FarmPesticide/Service.FarmPesticide.csproj", "src/Services/Service.FarmPesticide/"]
RUN dotnet restore "src/Services/Service.FarmPesticide/Service.FarmPesticide.csproj"
COPY . .
WORKDIR "/src/src/Services/Service.FarmPesticide"
RUN dotnet build "Service.FarmPesticide.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Service.FarmPesticide.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.FarmPesticide.dll"]