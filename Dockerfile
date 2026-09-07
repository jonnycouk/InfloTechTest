FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy full solution source code first so all project references resolve
COPY . .
RUN dotnet restore "UserManagement.Web/UserManagement.Web.csproj"

WORKDIR "/src/UserManagement.Web"
RUN dotnet build "./UserManagement.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserManagement.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY README.md .
COPY README.md /README.md
ENTRYPOINT ["dotnet", "UserManagement.Web.dll"]
