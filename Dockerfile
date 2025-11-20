# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore
COPY *.sln .
COPY SHC.Presentation/*.csproj ./SHC.Presentation/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/SHC.Presentation
RUN dotnet publish -c Release -o out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/SHC.Presentation/out ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "SHC.Presentation.dll"]
