FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/PetWorld.Domain/PetWorld.Domain.csproj", "PetWorld.Domain/"]
COPY ["src/PetWorld.Application/PetWorld.Application.csproj", "PetWorld.Application/"]
COPY ["src/PetWorld.Infrastructure/PetWorld.Infrastructure.csproj", "PetWorld.Infrastructure/"]
COPY ["src/PetWorld.Web/PetWorld.Web.csproj", "PetWorld.Web/"]
RUN dotnet restore "PetWorld.Web/PetWorld.Web.csproj"
COPY src/ .
WORKDIR "/src/PetWorld.Web"
RUN dotnet build "PetWorld.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetWorld.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "PetWorld.Web.dll"]
