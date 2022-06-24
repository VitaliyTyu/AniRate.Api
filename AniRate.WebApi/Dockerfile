FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AniRate.WebApi/AniRate.WebApi.csproj", "AniRate.WebApi/"]
COPY ["AniRate.Application/AniRate.Application.csproj", "AniRate.Application/"]
COPY ["AniRate.Domain/AniRate.Domain.csproj", "AniRate.Domain/"]
COPY ["AniRate.Infrastructure/AniRate.Infrastructure.csproj", "AniRate.Infrastructure/"]

RUN dotnet restore "AniRate.WebApi/AniRate.WebApi.csproj"
COPY . .
WORKDIR "/src/AniRate.WebApi"
RUN dotnet build "AniRate.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AniRate.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AniRate.WebApi.dll"]




#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#EXPOSE 80
#EXPOSE 443
#WORKDIR /app
#
## copy csproj and restore as distinct layers
#COPY *.csproj ./
#RUN dotnet restore
#
## copy everything else and build
#COPY . ./
#RUN dotnet publish -c Release -o out
#
## build runtime image
#FROM mcr.microsoft.com/dotnet/runtime:6.0 
#WORKDIR /app
#COPY --from=build-env /app/out ./
#ENTRYPOINT ["dotnet", "AniRate.WebApi.dll"]