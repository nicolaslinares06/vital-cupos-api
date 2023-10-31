FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API/API.csproj", "API/"]
COPY ["Repository/Repository.csproj", "Repository/"]
RUN dotnet restore "API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Copiar los archivos del certificado y la clave privada al contenedor desde la carpeta 'certs'
COPY certs/localhost.crt /app/localhost.crt
COPY certs/localhost.key /app/localhost.key

# Configurar el certificado SSL en el contenedor
ENV ASPNETCORE_URLS=https://+:443;http://+:80
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/localhost.crt
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=
ENV ASPNETCORE_Kestrel__Certificates__Default__KeyPath=/app/localhost.key

# Copiar el contenido publicado de la aplicación
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "API.dll" , "--environment=Development"]
