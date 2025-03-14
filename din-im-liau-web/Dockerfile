FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["din-im-liau-web/din-im-liau-web.csproj", "din-im-liau-web/"]
RUN dotnet restore "din-im-liau-web/din-im-liau-web.csproj"
COPY . .
WORKDIR "/src/din-im-liau-web"
RUN dotnet build "din-im-liau-web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "din-im-liau-web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "din-im-liau-web.dll"]
