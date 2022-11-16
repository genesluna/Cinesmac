FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "Cinesmac.sln" --disable-parallel
RUN dotnet publish "Cinesmac.sln" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "API.dll"]