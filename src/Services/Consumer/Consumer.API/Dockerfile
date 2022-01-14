#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Consumer/Consumer.API/Consumer.API.csproj", "Services/Consumer/Consumer.API/"]
RUN dotnet restore "Services/Consumer/Consumer.API/Consumer.API.csproj"
COPY . .
WORKDIR "/src/Services/Consumer/Consumer.API"
RUN dotnet build "Consumer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Consumer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Consumer.API.dll"]
