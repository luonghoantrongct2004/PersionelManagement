# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_NOLOGO=1
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Copy the published output from a previous build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/BE.NETCore/BE.NETCore.csproj", "src/BE.NETCore/"]
COPY ["src/BE.DAL/BE.DAL.csproj", "src/BE.DAL/"]
COPY ["src/BE.Domain/BE.Domain.csproj", "src/BE.Domain/"]
RUN dotnet restore "src/BE.NETCore/BE.NETCore.csproj"
COPY . .
WORKDIR "/src/src/BE.NETCore"
RUN dotnet build "BE.NETCore.csproj" --configuration Release --no-restore --nologo --output /app/build 

# Use the base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "BE.NETCore.dll"]