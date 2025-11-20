FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy the solution file first
COPY SHC.sln ./

# Copy all project folders
COPY SHC.Presentation/ SHC.Presentation/
COPY SHC.Core.Domain/ SHC.Core.Domain/
COPY SHC.Core.Services/ SHC.Core.Services/
COPY SHC.Application/ SHC.Application/
COPY SHC.Infrastructure.Data/ SHC.Infrastructure.Data/
COPY SHC.Core.Interfaces/ SHC.Core.Interfaces/
COPY SHC.Infrastructure.Security/ SHC.Infrastructure.Security/
COPY SHC.Core.Projections/ SHC.Core.Projections/
COPY SHC.Infrastructure.Models/ SHC.Infrastructure.Models/

# Restore dependencies
RUN dotnet restore

# Copy everything else (if any)
COPY . .

# Build the solution
RUN dotnet build -c Release -o /app/build

# Publish
RUN dotnet publish SHC.Presentation/SHC.Presentation.csproj -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SHC.Presentation.dll"]
