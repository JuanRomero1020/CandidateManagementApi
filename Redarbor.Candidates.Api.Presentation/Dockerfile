#The individual restore layers optimize the cointainer build and upload
#If no dependencies are changed in the layer the layer isn't re-builded and docker uses the cached layer 
FROM build AS domain-restore
COPY ["Redarbor.Candidates.Api.Domain/Redarbor.Candidates.Api.Domain.csproj", "Redarbor.Candidates.Api.Domain/Redarbor.Candidates.Api.Domain.csproj"]
RUN dotnet restore "Redarbor.Candidates.Api.Domain/Redarbor.Candidates.Api.Domain.csproj"

FROM domain-restore AS business-restore
COPY ["Redarbor.Candidates.Api.Business/Redarbor.Candidates.Api.Business.csproj", "Redarbor.Candidates.Api.Business/Redarbor.Candidates.Api.Business.csproj"]
RUN dotnet restore "Redarbor.Candidates.Api.Business/Redarbor.Candidates.Api.Business.csproj"

FROM business-restore AS infrastructure-restore
COPY ["Redarbor.Candidates.Api.Insrastructure/Redarbor.Candidates.Api.Infrastructure.csproj", "Redarbor.Candidates.Api.Insrastructure/Redarbor.Candidates.Api.Infrastructure.csproj"]
RUN dotnet restore "Redarbor.Candidates.Api.Insrastructure/Redarbor.Candidates.Api.Infrastructure.csproj"

FROM infrastructure-restore AS api-restore
COPY ["Redarbor.Candidates.Api.Presentation/Redarbor.Candidates.Api.Presentation.csproj", "Redarbor.Candidates.Api.Presentation/Redarbor.Candidates.Api.Presentation.csproj"]
RUN dotnet restore "Redarbor.Candidates.Api.Presentation/Redarbor.Candidates.Api.Presentation.csproj"

FROM api-restore AS publish
COPY ["Redarbor.Candidates.Api.Presentation/", "Redarbor.Candidates.Api.Presentation/"]
COPY ["Redarbor.Candidates.Api.Business/", "Redarbor.Candidates.Api.Business/"]
COPY ["Redarbor.Candidates.Api.Domain/", "Redarbor.Candidates.Api.Domain/"]
COPY ["Redarbor.Candidates.Api.Insrastructure/", "Redarbor.Candidates.Api.Insrastructure/"]
WORKDIR ./Redarbor.Candidates.Api.Presentation/
RUN dotnet publish "Redarbor.Candidates.Api.Presentation.csproj" -c Release -o /app/publish --no-restore 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Redarbor.Candidates.Api.Presentation.dll"]
