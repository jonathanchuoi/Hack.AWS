FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SNS.Producer/SNS.Producer.csproj", "SNS.Producer/"]
RUN dotnet restore "SNS.Producer/SNS.Producer.csproj"
COPY . .
WORKDIR "/src/SNS.Producer"
RUN dotnet build "SNS.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SNS.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SNS.Producer.dll"]
