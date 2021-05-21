#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build-env
WORKDIR /app

COPY . ./
WORKDIR /app/Fitverse.CalendarService
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /app
EXPOSE 5007
COPY --from=build-env /app/Fitverse.CalendarService/out .
ENTRYPOINT ["dotnet", "Fitverse.CalendarService.dll"]

