#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TDD .NET 6 API/TDD .NET 6 API.csproj", "TDD .NET 6 API/"]
RUN dotnet restore "TDD .NET 6 API/TDD .NET 6 API.csproj"
COPY . .
WORKDIR "/src/TDD .NET 6 API"
RUN dotnet build "TDD .NET 6 API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TDD .NET 6 API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TDD .NET 6 API.dll"]