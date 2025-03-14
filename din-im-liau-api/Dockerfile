FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["din-im-liau-api/din-im-liau-api.csproj", "din-im-liau-api/"]
RUN dotnet restore "din-im-liau-api/din-im-liau-api.csproj"
COPY . .
WORKDIR "/src/din-im-liau-api"
RUN dotnet build "din-im-liau-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "din-im-liau-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "din-im-liau-api.dll"]
