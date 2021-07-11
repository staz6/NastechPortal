# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src
# COPY ["Services/UserManagement/UserManagement.csproj", "Services/UserManagement/"]
# COPY ["ContractHandler/EventBus.Messages/EventBus.Messages.csproj","ContractHandler/EventBus.Messages/"]
# RUN dotnet restore "Services/UserManagement/UserManagement.csproj"
# COPY . .
# WORKDIR "/src/Services/UserManagement/"
# RUN dotnet build "UserManagement.csproj" -c Release -o /app/build

# FROM build As publish
# RUN dotnet publish "UserManagement.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet","UserManagement.dll"]

#FOR ATTENDANCE 

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/AttendanceManagement/AttendanceManagement.csproj", "Services/AttendanceManagement/"]
COPY ["ContractHandler/EventBus.Messages/EventBus.Messages.csproj","ContractHandler/EventBus.Messages/"]
RUN dotnet restore "Services/AttendanceManagement/AttendanceManagement.csproj"
COPY . .
WORKDIR "/src/Services/AttendanceManagement/"
RUN dotnet build "AttendanceManagement.csproj" -c Release -o /app/build

FROM build As publish
RUN dotnet publish "AttendanceManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","AttendanceManagement.dll"]

#FOR SALARY

# FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
# WORKDIR /app
# EXPOSE 80

# FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# WORKDIR /src
# COPY ["Services/SalaryManagement/SalaryManagement.csproj", "Services/SalaryManagement/"]
# COPY ["ContractHandler/EventBus.Messages/EventBus.Messages.csproj","ContractHandler/EventBus.Messages/"]
# RUN dotnet restore "Services/SalaryManagement/SalaryManagement.csproj"
# COPY . .
# WORKDIR "/src/Services/SalaryManagement/"
# RUN dotnet build "SalaryManagement.csproj" -c Release -o /app/build

# FROM build As publish
# RUN dotnet publish "SalaryManagement.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet","SalaryManagement.dll"]