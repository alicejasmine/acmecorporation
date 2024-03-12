# Acme Corporation Prize Draw

# Overview

Welcome to the Acme Corporation Prize Draw Web Application! This application allows users to enter a draw for a prize by submitting valid serial numbers from Acme Corporation's products.
Users can enter the draw twice for each valid serial number, and they must be at least 18 years old.

# Project Structure

The backend is structured into the following layers:

- **API Layer:** Handles HTTP requests, interacting with the frontend and includes data validation with transfer model. 
- **Service Layer:** Implements business logic related to draw entry management, including validation and data retrieval.
- **Infrastructure Layer:** Manages infrastructure concerns such as database connections and database access layer.
-  **Tests:** Includes unit tests for entry creation with test cases: success, fail due to data validation and fail due to max 2 entries per serial number. 

The project utilizes dependency injection to manage dependencies between different components.

The frontend is developed using Angular + Tailwind CSS. It includes data validation using FormGroup and FormControls and it is structured into the following components:
 - **draw-landing-page component:** Accessible at http://localhost:4200
 - **form-submissions component:** Accessible at http://localhost:4200/entries 

# Database

The application uses SQL Server as the database, running in a Docker container using this image: https://hub.docker.com/_/microsoft-mssql-server
For enhanced security, the database connection string is retrieved from the "sqlconn" environment variable.  Refer to [Utilities.cs](./infrastructure/Utilities.cs) in the infrastructure folder for connection string details.

## Database tables:
The database and table used in the application can be found in [create_database.sql](create_database.sql)
Tables are created in the default schema dbo.

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

# Run the Project and Tests

**- backend:**
  - Navigate to the Api Directory:
    ```bash
    cd api
    ```

  - Run the .NET Application:
    ```bash
    dotnet run
    ```

**- frontend:**
  - Navigate to the Frontend Directory:
    ```bash
    cd frontend
    ```

  - Run the Angular Application:
    ```bash
    ng serve
    ```

  - Access:
    - [http://localhost:4200](http://localhost:4200)
    - [http://localhost:4200/entries](http://localhost:4200/entries)

**- tests (backend):**
  - To run tests for the backend, execute the following command:
    ```bash
    dotnet test
    ```

