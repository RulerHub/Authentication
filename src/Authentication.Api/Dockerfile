FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Authentication.Api/Authentication.Api.csproj", "src/Authentication.Api/"]
COPY ["shd/Authentication.Core/Authentication.Core.csproj", "shd/Authentication.Core/"]
COPY ["src/Authentication.Data/Authentication.Data.csproj", "src/Authentication.Data/"]
RUN dotnet restore "./src/Authentication.Api/Authentication.Api.csproj"
COPY . .
WORKDIR "/src/src/Authentication.Api"
RUN dotnet build "./Authentication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Authentication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Api.dll"]