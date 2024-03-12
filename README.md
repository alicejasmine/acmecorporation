# Acme Corporation Prize Draw

# Overview

Welcome to the Acme Corporation Prize Draw Web Application! This application allows users to enter a draw for a prize by submitting valid serial numbers from Acme Corporation's products.
Users can enter the draw twice for each valid serial number, and they must be at least 18 years old.

# Project Structure

The backend is structured into the following layers:

- **API Layer:** Handles HTTP requests, interacting with the frontend and includes data validation with transfer model. 
- **Service Layer:** Implements business logic related to draw entry management, including validation and data retrieval.
- **Infrastructure Layer:** Manages infrastructure concerns such as database connections and database access layer.
-  **Tests:** Includes unit tests for entry creation with test cases: success, fail due to data validation and fail due to maximum 2 entries per serial number limit. 

The project utilizes dependency injection to manage dependencies between different components.

The frontend is structured into the following components:
 - **draw-landing-page component:** Accessible at http://localhost:4200
 - **form-submissions component:** Accessible at http://localhost:4200/entries 
    

# Database

The application uses SQL Server as the database, running in a Docker container using this image: https://hub.docker.com/_/microsoft-mssql-server
For enhanced security, the database connection string is retrieved from the "sqlconn" environment variable.  Refer to [Utilities.cs](./infrastructure/Utilities.cs) in the infrastructure folder for connection string details.

## Database tables:

CREATE DATABASE AcmeDB;

USE AcmeDB
CREATE TABLE DrawEntries (
    entry_ID INT PRIMARY KEY IDENTITY(1,1),
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    email_address NVARCHAR(255) NOT NULL,
    serial_number NVARCHAR(20) NOT NULL
);


USE AcmeDB
CREATE TABLE ProductSerialNumbers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    serial_number NVARCHAR(20) NOT NULL UNIQUE);


## Running SQL Server with Docker
Here are the instructions to run SQL Server with Docker:

1. **Download Docker and Create a Docker Hub Account:**
   - https://www.docker.com/get-started
   - https://hub.docker.com
     
2. **Find the SQL Server Image:**
   - Visit the official Microsoft SQL Server image page on Docker Hub: [microsoft-mssql-server](https://hub.docker.com/_/microsoft-mssql-server).

3. **Copy the Docker Run Command:**
   - In the "How to use this image" section, copy the command for SQL Server 2022. Customize the password before running the command.
   
     ```bash
     docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Evaluation" -p 1433:1433 --name sqlpreview --hostname sqlpreview -d mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
     ```

4. **Open Terminal/Command Line:**
   - Open a terminal or command line on your machine.

5. **Login to Docker:**
   - Run the following command to log in to Docker using your Docker Hub credentials:

     ```bash
     docker login
     ```

6. **Run Docker Command:**
   - Execute the copied Docker run command, replacing the password and container name as needed. This command will download the SQL Server 2022 image and start the container.

     ```bash
     docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourNewPassword" -e "MSSQL_PID=Evaluation" -p 1433:1433 --name yourContainerName -d mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
     ```

   - Replace `yourNewPassword` with your desired strong password.
   - Replace `yourContainerName` with a preferred name for your SQL Server container.

8. **Access SQL Server:**
   - SQL Server should now be running in the Docker container. Connect to it using the specified port (default is 1433) and configured credentials.

# Run the project/tests
   - backend:
    cd api
    dotnet run

   - frontend:
     cd frontend
     ng serve
 Access http://localhost:4200 and http://localhost:4200/entries

     - tests:
       dotnet test

