FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TasksApp.csproj", "./"]
RUN dotnet restore "TasksApp.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "TasksApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TasksApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TasksApp.dll"]
