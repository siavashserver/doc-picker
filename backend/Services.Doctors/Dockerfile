FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services.Doctors/Services.Doctors.csproj", "Services.Doctors/"]
RUN dotnet restore "Services.Doctors/Services.Doctors.csproj"
COPY . .
WORKDIR "/src/Services.Doctors"
RUN dotnet build "Services.Doctors.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Doctors.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Doctors.dll"]
