#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/LogRWebApp/LogRWebApp.csproj", "src/LogRWebApp/"]
COPY ["src/LogRWebMonitor/LogRWebMonitor.csproj", "src/LogRWebMonitor/"]
COPY ["src/Plugins/OvhGrayLogR/OvhGrayLogR.csproj", "src/Plugins/OvhGrayLogR/"]
RUN dotnet restore "src/LogRWebApp/LogRWebApp.csproj"
COPY . .
WORKDIR "/src/src/LogRWebApp"
RUN dotnet build "LogRWebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogRWebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogRWebApp.dll"]