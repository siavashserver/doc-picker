FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /src

COPY ["Services.Accounts/Services.Accounts.csproj", "Services.Accounts/"]
COPY ["Services.Accounts.Shared/Services.Accounts.Shared.csproj", "Services.Accounts.Shared/"]
COPY ["Services.Doctors/Services.Doctors.csproj", "Services.Doctors/"]
COPY ["Services.Doctors.Shared/Services.Doctors.Shared.csproj", "Services.Doctors.Shared/"]
COPY ["Services.Reservations/Services.Reservations.csproj", "Services.Reservations/"]
COPY ["Services.Shared/Services.Shared.csproj", "Services.Shared/"]
COPY ["WebAPI.REST/WebAPI.REST.csproj", "WebAPI.REST/"]

RUN dotnet restore "Services.Accounts/Services.Accounts.csproj"
RUN dotnet restore "Services.Accounts.Shared/Services.Accounts.Shared.csproj"
RUN dotnet restore "Services.Doctors/Services.Doctors.csproj"
RUN dotnet restore "Services.Doctors.Shared/Services.Doctors.Shared.csproj"
RUN dotnet restore "Services.Reservations/Services.Reservations.csproj"
RUN dotnet restore "Services.Shared/Services.Shared.csproj"
RUN dotnet restore "WebAPI.REST/WebAPI.REST.csproj"

COPY . .

RUN dotnet publish "Services.Accounts/Services.Accounts.csproj" -c Release -o /app/publish/Services.Accounts /p:UseAppHost=false
RUN dotnet publish "Services.Doctors/Services.Doctors.csproj" -c Release -o /app/publish/Services.Doctors /p:UseAppHost=false
RUN dotnet publish "Services.Reservations/Services.Reservations.csproj" -c Release -o /app/publish/Services.Reservations /p:UseAppHost=false
RUN dotnet publish "WebAPI.REST/WebAPI.REST.csproj" -c Release -o /app/publish/WebAPI.REST /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

FROM runtime AS services.accounts
WORKDIR /app
COPY --from=publish /app/publish/Services.Accounts .
ENTRYPOINT ["dotnet", "Services.Accounts.dll"]

FROM runtime AS services.doctors
WORKDIR /app
COPY --from=publish /app/publish/Services.Doctors .
ENTRYPOINT ["dotnet", "Services.Doctors.dll"]

FROM runtime AS services.reservations
WORKDIR /app
COPY --from=publish /app/publish/Services.Reservations .
ENTRYPOINT ["dotnet", "Services.Reservations.dll"]

FROM runtime AS webapi.rest
WORKDIR /app
COPY --from=publish /app/publish/WebAPI.REST .
ENTRYPOINT ["dotnet", "WebAPI.REST.dll"]
