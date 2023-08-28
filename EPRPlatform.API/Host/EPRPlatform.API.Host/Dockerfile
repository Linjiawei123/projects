#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Host/EPRPlatform.API.Host/EPRPlatform.API.Host.csproj", "Host/EPRPlatform.API.Host/"]
COPY ["Infrastructure/EPRPlatform.API.Dto/EPRPlatform.API.Dto.csproj", "Infrastructure/EPRPlatform.API.Dto/"]
COPY ["Infrastructure/EPRPlatform.API.Method/EPRPlatform.API.Method.csproj", "Infrastructure/EPRPlatform.API.Method/"]
COPY ["Infrastructure/EPRPlatform.API.Models/EPRPlatform.API.Models.csproj", "Infrastructure/EPRPlatform.API.Models/"]
COPY ["Infrastructure/EPRPlatform.API.Dictionary/EPRPlatform.API.Dictionary.csproj", "Infrastructure/EPRPlatform.API.Dictionary/"]
COPY ["Infrastructure/EPRPlatform.API.Extend/EPRPlatform.API.Extend.csproj", "Infrastructure/EPRPlatform.API.Extend/"]
COPY ["Infrastructure/EPRPlatform.API.Interfaces/EPRPlatform.API.Interfaces.csproj", "Infrastructure/EPRPlatform.API.Interfaces/"]
COPY ["Infrastructure/EPRPlatform.API.Repository/EPRPlatform.API.Repository.csproj", "Infrastructure/EPRPlatform.API.Repository/"]
RUN dotnet restore "Host/EPRPlatform.API.Host/EPRPlatform.API.Host.csproj"
COPY . .
WORKDIR "/src/Host/EPRPlatform.API.Host"
RUN dotnet build "EPRPlatform.API.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EPRPlatform.API.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EPRPlatform.API.Host.dll"]