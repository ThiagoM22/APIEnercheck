# Estágio 1: Base (Runtime)
# Essa imagem é leve e serve apenas para rodar a aplicação final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Estágio 2: Build (SDK)
# Essa imagem contém todas as ferramentas para compilar o código
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copia o arquivo de projeto e restaura as dependências
COPY ["APIEnercheck/APIEnercheck.csproj", "APIEnercheck/"]
RUN dotnet restore "./APIEnercheck/APIEnercheck.csproj"
# Copia todo o resto do código
COPY . .
WORKDIR "/src/APIEnercheck"
# Compila a aplicação
RUN dotnet build "./APIEnercheck.csproj" -c $BUILD_CONFIGURATION -o /app/build


# Estágio 3: Publish
# Publica a aplicação gerando os arquivos finais (DLLs)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./APIEnercheck.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
ENV GEMINI_API_KEY="AIzaSyAdgZhnTeEh2h_CE-KYR1vLn9ZyA1CwDB0"

# Estágio 4: Final
# Copia os arquivos publicados para a imagem base leve
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIEnercheck.dll"]
