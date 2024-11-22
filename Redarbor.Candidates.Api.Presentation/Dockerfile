FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Redarbor.Candidates.Api.Presentation/Redarbor.Candidates.Api.Presentation.csproj", "Redarbor.Candidates.Api.Presentation/"]
RUN dotnet restore "Redarbor.Candidates.Api.Presentation/Redarbor.Candidates.Api.Presentation.csproj"
COPY . .
WORKDIR "/src/Redarbor.Candidates.Api.Presentation"
RUN dotnet build "Redarbor.Candidates.Api.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Redarbor.Candidates.Api.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Redarbor.Candidates.Api.Presentation.dll"]