#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 44311

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DomecChallange/DomecChallange.csproj", "DomecChallange/"]
COPY ["DomeChallange.Infrastructure/DomeChallange.Infrastructure.csproj", "DomeChallange.Infrastructure/"]
COPY ["DomecChallange.Mapper/DomecChallange.Mapper.csproj", "DomecChallange.Mapper/"]
COPY ["DomecChallange.Domain/DomecChallange.Domain.csproj", "DomecChallange.Domain/"]
COPY ["DomecChallange.Dtos/DomecChallange.Dtos.csproj", "DomecChallange.Dtos/"]
COPY ["DomecChallange.Service/DomecChallange.Service.csproj", "DomecChallange.Service/"]
COPY ["DomecChallange.Data/DomecChallange.Data.csproj", "DomecChallange.Data/"]
RUN dotnet restore "DomecChallange/DomecChallange.csproj"
COPY . .
WORKDIR "/src/DomecChallange"
RUN dotnet build "DomecChallange.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DomecChallange.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomecChallange.dll"]