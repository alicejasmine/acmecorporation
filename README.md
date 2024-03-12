# Acme Corporation Prize Draw

## Overview

Welcome to the Acme Corporation Prize Draw Web Application! This application allows users to enter a draw for a prize by submitting valid serial numbers from Acme Corporation's products.
Users can enter the draw twice for each valid serial number, and they must be at least 18 years old.

## Project Structure

The backend is structured into the following layers:

- **API Layer:** Handles HTTP requests, interacting with the frontend and includes data validation with transfer model. 
- **Service Layer:** Implements business logic related to draw entry management, including validation and data retrieval.
- **Infrastructure Layer:** Manages infrastructure concerns such as database connections and database access layer.
-  **Tests:** Includes unit tests for entry creation with test cases: success, fail due to data validation and fail due to maximum 2 entries per serial number limit. 

The project utilizes dependency injection to manage dependencies between different components.

The frontend is structured into the following components:
 - **draw-landing-page component:** Accessible at http://localhost:4200
 - **form-submissions component:** Accessible at http://localhost:4200/entries 
    

## Database

The application uses SQL Server as the database, running in a Docker container using this image: https://hub.docker.com/_/microsoft-mssql-server
For enhanced security, the database connection string is retrieved from the "sqlconn" environment variable.  Refer to [Utilities.cs](./infrastructure/Utilities.cs) in the infrastructure folder for connection string details.

Here are the instructions to run SQL Server with Docker:
- Download Docker, set up an account on Docker Hub.
- Find image on Docker hub: https://hub.docker.com/_/microsoft-mssql-server
- In the How to use this image section, copy this command for SQL Server 2022: (You can change the password before running the command)
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Evaluation" -p 1433:1433  --name sqlpreview --hostname sqlpreview -d mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
- Open a terminal/cmd line
- login into docker using docker login
- run command from above with changed password and a name for the container to download the image.



