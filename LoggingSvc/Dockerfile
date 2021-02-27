#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["LoggingSvc/LoggingSvc.csproj", "LoggingSvc/"]
RUN dotnet restore "LoggingSvc/LoggingSvc.csproj"
COPY . .
WORKDIR "/src/LoggingSvc"
RUN dotnet build "LoggingSvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoggingSvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoggingSvc.dll"]