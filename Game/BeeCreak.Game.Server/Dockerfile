FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY BeeCreak.Game.Server.csproj ./
RUN dotnet restore "BeeCreak.Game.Server.csproj"
COPY . .
RUN dotnet publish "BeeCreak.Game.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 7777/udp
EXPOSE 8080/tcp
ENTRYPOINT ["dotnet", "BeeCreak.Game.Server.dll"]
