FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
RUN mkdir -p wwwroot/
RUN mkdir -p wwwroot/Repository/
RUN mkdir -p wwwroot/Repository/apk
RUN mkdir -p wwwroot/Repository/icons
RUN mkdir -p wwwroot/Repository/images
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EbinApi/EbinApi.csproj", "EbinApi/"]
RUN dotnet restore "EbinApi/EbinApi.csproj"
COPY . .

WORKDIR "/src/EbinApi"
RUN dotnet build "EbinApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EbinApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EbinApi.dll"]