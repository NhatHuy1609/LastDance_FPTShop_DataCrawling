FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else
COPY . ./

# Create the directory for the certificate
RUN mkdir -p /https

# Generate a self-signed certificate for HTTPS
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p password
RUN dotnet dev-certs https --trust

ENTRYPOINT ["dotnet", "watch", "--launch-profile", "https"]