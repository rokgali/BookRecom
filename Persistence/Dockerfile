# Use the official Microsoft SQL Server image from the Microsoft Container Registry
FROM mcr.microsoft.com/mssql/server:2022-latest

# Set environment variables required for SQL Server
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=StrongPass!123
ENV MSSQL_DATABASE="bookrecom"

# Expose SQL Server port
EXPOSE 1433

# Run SQL Server process (the entrypoint is already defined in the base image)
CMD ["/opt/mssql/bin/sqlservr"]
