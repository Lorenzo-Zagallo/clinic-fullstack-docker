# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restaura dependências copiando apenas os arquivos de projeto primeiro (otimização de cache)
COPY ["Clinica.API/Clinica.API.csproj", "Clinica.API/"]
COPY ["Clinica.Application/Clinica.Application.csproj", "Clinica.Application/"]
COPY ["Clinica.Domain/Clinica.Domain.csproj", "Clinica.Domain/"]
COPY ["Clinica.Infrastructure/Clinica.Infrastructure.csproj", "Clinica.Infrastructure/"]
RUN dotnet restore "Clinica.API/Clinica.API.csproj"

# Copia o restante do código e realiza a compilação final
COPY . .
WORKDIR "/src/Clinica.API"
RUN dotnet publish "Clinica.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# MUDANÇA CRÍTICA DE SEGURANÇA: Usuário não-root
USER $APP_UID

# O contêiner vai escutar na porta 8080 (padrão seguro do ASP.NET 8+)
EXPOSE 8080

ENTRYPOINT ["dotnet", "Clinica.API.dll"]