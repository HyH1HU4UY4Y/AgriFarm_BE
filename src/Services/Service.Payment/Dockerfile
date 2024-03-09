#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Service.Payment/Service.Payment.csproj", "src/Services/Service.Payment/"]
COPY ["src/Core/Common/SharedApplication.csproj", "src/Core/Common/"]
COPY ["src/Core/Domain/SharedDomain.csproj", "src/Core/Domain/"]
COPY ["src/Infrastructure/Infrastructure.Payment/Infrastructure.Payment.csproj", "src/Infrastructure/Infrastructure.Payment/"]
COPY ["src/Services/Service.RiskAssessment/Service.RiskAssessment.csproj", "src/Services/Service.RiskAssessment/"]
COPY ["src/Infrastructure/Infrastructure.RiskAssessment/Infrastructure.RiskAssessment.csproj", "src/Infrastructure/Infrastructure.RiskAssessment/"]
RUN dotnet restore "./src/Services/Service.Payment/./Service.Payment.csproj"
COPY . .
WORKDIR "/src/src/Services/Service.Payment"
RUN dotnet build "./Service.Payment.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Service.Payment.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.Payment.dll"]