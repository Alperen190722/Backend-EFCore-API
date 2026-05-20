
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src


COPY ["D41-SkylinePayroll.Api/D41-SkylinePayroll.Api.csproj", "D41-SkylinePayroll.Api/"]
RUN dotnet restore "./D41-SkylinePayroll.Api/D41-SkylinePayroll.Api.csproj"


COPY . .
RUN dotnet publish "D41-SkylinePayroll.Api/D41-SkylinePayroll.Api.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .


ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "D41-SkylinePayroll.Api.dll"]