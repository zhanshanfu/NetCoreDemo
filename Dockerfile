FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NetCoreDemo/NetCoreDemo.csproj", "NetCoreDemo/"]
COPY ["NetCoreDemo.Service/NetCoreDemo.Service.csproj", "NetCoreDemo.Service/"]
COPY ["NetCoreDemo.DB/NetCoreDemo.DB.csproj", "NetCoreDemo.DB/"]
COPY ["NetCoreDemo.Tools/NetCoreDemo.Tools.csproj", "NetCoreDemo.Tools/"]
COPY ["NetCoreDemo.Entity/NetCoreDemo.Entity.csproj", "NetCoreDemo.Entity/"]
RUN dotnet restore "NetCoreDemo/NetCoreDemo.csproj"
COPY . .
WORKDIR "/src/NetCoreDemo"
RUN dotnet build "NetCoreDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreDemo.dll"]