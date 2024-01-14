FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services.Reservations/Services.Reservations.csproj", "Services.Reservations/"]
RUN dotnet restore "Services.Reservations/Services.Reservations.csproj"
COPY . .
WORKDIR "/src/Services.Reservations"
RUN dotnet build "Services.Reservations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Reservations.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Reservations.dll"]
