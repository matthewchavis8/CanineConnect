FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN dotnet workload install maui

WORKDIR /src
COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -f net7.0 \
    -r linux-x64 --self-contained false \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:7.0

RUN apt-get update && \
    apt-get install -y --no-install-recommends \
      libgtk-3-0 libwebkit2gtk-4.0-37 && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CanineConnect.dll"]
