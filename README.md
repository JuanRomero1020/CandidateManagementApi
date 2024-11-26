# Candidate Management API

Welcome to the **Candidate Management API**! This project allows you to manage candidates and their experiences efficiently, with a well-structured REST API built using **ASP.NET Core** and **Entity Framework**. The API includes functionality for adding, updating, and deleting candidate data, with proper validation for the fields.

---

## Features

- **Candidate CRUD Operations**: Add, update, retrieve, and delete candidate information.
- **Experience Management**: Update and insert candidate experiences.
- **Validation**: Ensure candidate and experience data integrity with **FluentValidation**.
- **Error Handling & Logging**: Integrated logging and error handling with **Serilog**.
- **Database Setup**: Includes SQL scripts to set up the database and test data.

---

## Table of Contents

- [Installation](#installation)
- [Database Setup](#database-setup)
- [Usage](#usage)
- [API Endpoints](#endpoints)
- [Validations](#validations)
- [Contributing](#contributing)
- [License](#license)

---

## Installation

To get the project up and running locally, follow the steps below:

### Prerequisites

- [.NET 6.0 SDK or higher](https://dotnet.microsoft.com/download/dotnet)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or Docker with SQL Server image.
- **Serilog** for logging.
- **FluentValidation** for validation.

### Steps

1. **Clone the repository:**

```bash
git clone https://github.com/yourusername/candidate-management-api.git
cd candidate-management-api
```
2. **Navigate into the project directory**:
```bash
cd repository-name
```
4. **Install dependencies**:
```bash
dotnet restore
```
5. **Build the solution**:
```bash
dotnet build
```
7. **Run the project locally**:
```bash
dotnet run
```
9. **To use Docker for SQL Server, you can follow the instructions in the Docker Compose Setup**:
  - 9.1 Ensure Docker is installed on your machine. If not, follow Docker's installation guide.
  - 9.2 Navigate to your project directory and run:
```bash
docker-compose up
```
You need to be in the folder that contains the file.
```code
version: '3.8'

services:

  database:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: candidates-database
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=Password123
    ports:
      - "1433:1433" 
    volumes:
      - candidates-db-data:/var/opt/mssql 
    networks:
      - candidates-network

networks:
  candidates-network:
    driver: bridge

volumes:
  candidates-db-data:
    driver: local

```

## Database Setup

This section will guide you through setting up the database using **Entity Framework** Core.

### Steps to Create the Database with Entity Framework

1. **Install Entity Framework Core:**

   First, make sure you have the necessary NuGet packages installed in your project.

   You can install these packages using the following commands:

   ```bash
   dotnet add package Microsoft.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools

   go to the folder “Redarbor.Candidates.Api.Redarbor.Candidates.Api.Infrastructure.DBContex”, open the terminal and you can execute: \
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
2. **SQL Commands**:
    you can use the option with the following sql commands to create the tables 
    
You can create the necessary database tables using the following SQL script:

```sql
-- Create 'candidates' table
IF OBJECT_ID('dbo.candidates', 'U') IS NOT NULL
    DROP TABLE dbo.candidates;

CREATE TABLE dbo.candidates (
    IdCandidate INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50),
    Surname VARCHAR(150),
    Birthdate DATETIME,
    Email VARCHAR(250) UNIQUE,
    InsertDate DATETIME,
    ModifyDate DATETIME NULL
);

-- Create 'candidateexperiences' table
IF OBJECT_ID('dbo.candidateexperiences', 'U') IS NOT NULL
    DROP TABLE dbo.candidateexperiences;

CREATE TABLE dbo.candidateexperiences (
    IdCandidateExperience INT IDENTITY(1,1) PRIMARY KEY,
    IdCandidate INT FOREIGN KEY REFERENCES dbo.candidates(IdCandidate),
    Company VARCHAR(100),
    Job VARCHAR(100),
    Description VARCHAR(4000),
    Salary NUMERIC(8,2),
    BeginDate DATETIME,
    EndDate DATETIME NULL,
    InsertDate DATETIME,
    ModifyDate DATETIME NULL
);
```

## Usage

Once the application is running, you can interact with the Candidate Management API via the following routes:

- `POST /candidates` - Create a new candidate.
- `GET /candidates/{id}` - Get a candidate by ID.
- `PUT /candidates/{id}` - Update an existing candidate.
- `DELETE /candidates/{id}` - Delete a candidate.

### Example Request: Create a Candidate

```bash
POST /candidates
Content-Type: application/json

{
  "name": "John Doe",
  "surname": "Doe",
  "birthdate": "1985-06-15",
  "email": "john.doe@example.com",
  "experiences": [
    {
      "company": "Tech Corp",
      "job": "Software Engineer",
      "description": "Developing web applications.",
      "salary": 70000.00,
      "beginDate": "2010-05-01",
      "endDate": "2015-06-30"
    }
```
### Example Response Error:

```json
{
    "errorMessage": "Internal server error, try again.",
    "errorCode": "500"
}
```
### Example Response Error:

```json
{
    "messageResponse": "Candidate action successfully"
}
```

### Validations

#### Candidate Validator

- **Name**: Required, minimum length of 3 characters.
- **Surname**: Required, minimum length of 3 characters.
- **Email**: Required, must be a valid email address.
- **Birthdate**: Required, cannot be in the future.
- **Experiences**: Each experience is validated with the `CandidateExperienceValidator`.

#### Candidate Experience Validator

- **Company**: Required, minimum length of 3 characters.
- **Job**: Required, minimum length of 3 characters.
- **Salary**: Must be greater than zero.
- **BeginDate**: Required, cannot be in the future.
- **EndDate**: If provided, must be after `BeginDate`.


## Endpoints
Here are the available endpoints for interacting with the Candidates API:

| HTTP Method | Endpoint                 | Description                                 |
|-------------|--------------------------|---------------------------------------------|
| `GET`       | `/candidates`             | Retrieve all candidates.                    |
| `POST`      | `/candidates`             | Create a new candidate.                     |
| `GET`       | `/candidates/{id}`        | Get a candidate by ID.                      |
| `PUT`       | `/candidates/{id}`        | Update an existing candidate.               |
| `DELETE`    | `/candidates/{id}`        | Delete a candidate.                         |
| `POST`      | `/candidates/experience`  | Add or update candidate experience data.    |

For detailed request and response formats, see [Validations](Validations.md).


## Contributing

We welcome contributions to the Candidate Management API! To ensure smooth collaboration, please follow the guidelines below:

### Steps to Contribute

1. **Fork the repository**:
   - Click the **Fork** button at the top-right of the repository to create a personal copy of the project.

2. **Clone your forked repository**:
   ```bash
   git clone https://github.com/your-username/candidate-management-api.git
   cd candidate-management-api
3.  **Create a new branch:**:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## License

The Candidate Management API is licensed under the **MIT License**.

### MIT License Summary

- **Permission**: You can use, modify, and distribute the software for personal, educational, or commercial purposes.
- **Limitation**: The software is provided "as is" without any warranties or guarantees.

For more details, check the full license text:


