FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["din-im-liau-admin/din-im-liau-admin.csproj", "din-im-liau-admin/"]
RUN dotnet restore "din-im-liau-admin/din-im-liau-admin.csproj"
COPY . .
WORKDIR "/src/din-im-liau-admin"
RUN dotnet build "din-im-liau-admin.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "din-im-liau-admin.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "din-im-liau-admin.dll"]
