# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project files into the working directory
COPY . .

# Restore the dependencies for the project
RUN dotnet restore

# Build the project
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory in the runtime image
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

# Expose the port that the app will run on
EXPOSE 8080

# Run the app
ENTRYPOINT ["dotnet", "backend.dll"]
