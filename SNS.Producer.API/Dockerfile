FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5261
ENV ASPNETCORE_URLS=http://+:5261
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SNS.Producer.API/SNS.Producer.API.csproj", "SNS.Producer.API/"]
COPY ["SNS.Infrastructure/SNS.Infrastructure.csproj", "SNS.Infrastructure/"]
COPY ["SQS.Infrastructure/SQS.Infrastructure.csproj", "SQS.Infrastructure/"]

RUN dotnet restore "SNS.Producer.API/SNS.Producer.API.csproj"
COPY . .
WORKDIR "/src/SNS.Producer.API"
RUN dotnet build "SNS.Producer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SNS.Producer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SNS.Producer.API.dll"]
