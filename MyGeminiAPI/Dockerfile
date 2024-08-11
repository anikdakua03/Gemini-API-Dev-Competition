FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyGeminiAPI.csproj", "MyGeminiAPI/"]
RUN dotnet restore "MyGeminiAPI/MyGeminiAPI.csproj"
COPY . ./MyGeminiAPI
WORKDIR "/src/MyGeminiAPI"
RUN dotnet build "MyGeminiAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MyGeminiAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyGeminiAPI.dll"]