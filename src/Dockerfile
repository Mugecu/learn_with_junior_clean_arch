FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0.302 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /
COPY ["./", "./"]
RUN dotnet restore "egisz-receive-residue.API/egisz-receive-residue.API.csproj"
WORKDIR "egisz-receive-residue.API"
RUN dotnet build "egisz-receive-residue.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "egisz-receive-residue.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "egisz-receive-residue.API.dll"]