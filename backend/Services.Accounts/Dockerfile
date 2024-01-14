FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services.Accounts/Services.Accounts.csproj", "Services.Accounts/"]
RUN dotnet restore "Services.Accounts/Services.Accounts.csproj"
COPY . .
WORKDIR "/src/Services.Accounts"
RUN dotnet build "Services.Accounts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Accounts.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Accounts.dll"]
