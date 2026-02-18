FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY BeeCreak.Server.Worker.csproj ./
RUN dotnet restore "BeeCreak.Server.Worker.csproj"
COPY . .
RUN dotnet publish "BeeCreak.Server.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 7777/udp
EXPOSE 8080/tcp
ENTRYPOINT ["dotnet", "BeeCreak.Server.Worker.dll"]
