FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ARG VERSION
ARG BUILD_DATE
ENV VERSION=$VERSION
ENV BUILD_DATE=$BUILD_DATE
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
ARG FEED_ACCESSTOKEN

RUN curl -L https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | sh

COPY ./O365Automation/O365Automation.csproj .
COPY ./NuGet.config .
ARG FEED_ACCESSTOKEN
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS="{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/drawbridge/_packaging/drawbridge/nuget/v3/index.json\", \"username\":\"docker\", \"password\":\"${FEED_ACCESSTOKEN}\"}]}"
RUN dotnet restore

COPY . .

RUN dotnet build ./O365Automation/O365Automation.csproj

FROM build AS publish
WORKDIR /src/O365Automation
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN apt-get update && apt-get install -y wget

ENTRYPOINT dotnet /app/O365Automation.dll