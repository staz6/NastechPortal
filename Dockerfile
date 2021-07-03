FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/UserManagment/UserManagment.csproj", "Services/UserManagment/"]
COPY ["ContractHandler/EventBus.Messages/EventBus.Messages.csproj","ContractHandler/EventBus.Messages/"]
RUN dotnet restore "Services/UserManagment/UserManagment.csproj"
COPY . .
WORKDIR "/src/Services/UserManagment/"
RUN dotnet build "UserManagment.csproj" -c Release -o /app/build

FROM build As publish
RUN dotnet publish "UserManagment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet","UserManagment.dll"]