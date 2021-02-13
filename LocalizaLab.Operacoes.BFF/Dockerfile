#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["LocalizaLab.Operacoes.BFF/LocalizaLab.Operacoes.BFF.csproj", "LocalizaLab.Operacoes.BFF/"]
RUN dotnet restore "LocalizaLab.Operacoes.BFF/LocalizaLab.Operacoes.BFF.csproj"
COPY . .
WORKDIR "/src/LocalizaLab.Operacoes.BFF"
RUN dotnet build "LocalizaLab.Operacoes.BFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LocalizaLab.Operacoes.BFF.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocalizaLab.Operacoes.BFF.dll"]