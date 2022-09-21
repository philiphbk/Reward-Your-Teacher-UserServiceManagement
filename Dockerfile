#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RYTUserManagementService.API/RYTUserManagementService.API.csproj", "RYTUserManagementService.API/"]
COPY ["RYTUserManagementService.Core/RYTUserManagementService.Core.csproj", "RYTUserManagementService.Core/"]
COPY ["RYTUserManagementService.Domain/RYTUserManagementService.Domain.csproj", "RYTUserManagementService.Domain/"]
COPY ["RYTUserManagementService.Dto/RYTUserManagementService.Dto.csproj", "RYTUserManagementService.Dto/"]
COPY ["RYTUserManagementService.Models/RYTUserManagementService.Models.csproj", "RYTUserManagementService.Models/"]
COPY ["RYTUserManagementService.Common.Utilities/RYTUserManagementService.Common.Utilities.csproj", "RYTUserManagementService.Common.Utilities/"]
COPY ["RYTUserManagementService.Test/RYTUserManagementService.Test.csproj", "RYTUserManagementService.Test/"]
RUN dotnet restore "RYTUserManagementService.API/RYTUserManagementService.API.csproj"
COPY . .
WORKDIR "/src/RYTUserManagementService.API"
RUN dotnet build "RYTUserManagementService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RYTUserManagementService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet RYTUserManagementService.API.dll
#ENTRYPOINT ["dotnet", "RYTUserManagementService.API.dll"]
