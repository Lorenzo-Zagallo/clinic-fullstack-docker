# 🏥 Clinic API - Clean Architecture & .NET 10

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-Code%20First-blue)](https://docs.microsoft.com/ef/core/)
[![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?logo=docker&logoColor=white)](https://www.docker.com/)
[![Tests](https://img.shields.io/badge/Tests-xUnit-success)](https://xunit.net/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

A modern RESTful API for clinical management developed with **.NET 10 (LTS)**. This project demonstrates the practical application of **Clean Architecture**, **SOLID principles**, and **Automated Testing**. 

It was built to modernize a legacy system (previously on Web Forms), migrating it to the latest, highly scalable, testable, and performant structure of the Microsoft ecosystem.

---

## 🚀 Technologies & Practices

- **.NET 10 SDK**: Utilizing the latest and most performant Long-Term Support (LTS) version.
- **Clean Architecture**: Clear separation of concerns (Domain, Application, Infrastructure, API).
- **Entity Framework Core**: Code-First ORM for robust data manipulation.
- **Docker & Docker Compose**: Fully containerized environment orchestrating the Database, API, and Frontend.
- **xUnit & Moq**: Automated unit testing and mocking for quality assurance.
- **JWT (JSON Web Token)**: Secure authentication and endpoint authorization via Bearer Tokens.
- **SQL Server**: Relational database management.
- **Swagger UI**: Interactive API documentation.
- **Dependency Injection**: Native .NET decoupling mechanism.

## 🏗️ Project Architecture

The solution is divided into highly decoupled layers to ensure maintainability and testability:

1. **Domain**: The core of the system. Contains Entities (`Paciente`, `Usuario`) and Repository Interfaces. Zero external dependencies.
2. **Application**: Use cases and business rules. Contains DTOs (`LoginDTO`, `CriarPacienteDTO`), Service Interfaces, and Implementations (`PacienteService`).
3. **Infrastructure**: Technical implementation. Contains the Database Context (`DbContext`), Migrations, and concrete Repositories (`PacienteRepository`).
4. **API**: The entry point. RESTful Controllers, JWT configuration, and Dependency Injection bootstrapping (`Program.cs`).
5. **Tests**: A dedicated project for automated unit testing to validate business rules.

## ✨ Key Features

- **Dashboard Analytics**: Optimized endpoints utilizing LINQ and Entity Framework to perform aggregations (Sum, Count, Average) directly in the database, returning lightweight DTOs for maximum performance.
- **Containerized Ecosystem**: The API seamlessly connects to a containerized SQL Server and serves data to an Angular frontend, all orchestrated via Docker Compose.

## 🧪 Testing & Quality Assurance

The project features a suite of unit tests to validate critical business logic.

* **xUnit**: The primary testing framework.
* **Moq**: Used to mock dependencies (e.g., Repositories) to isolate and test business logic accurately.

## ⚙️ How to Run

You can run this project either using Docker (Recommended) or locally using the .NET CLI.

### 🐳 Option A: Running with Docker (Recommended)

This project includes a `docker-compose.yml` file that orchestrates the SQL Server database, the .NET Backend, and the Angular Frontend automatically.

**Prerequisites:** Docker and Docker Desktop installed.

1. Clone the repository:
   ```bash
   git clone https://github.com/Lorenzo-Zagallo/clinica-api-dotnet-clean-arch.git
   cd clinica-api-dotnet-clean-arch
   ```
2. Build and start the containers in detached mode:
   ```bash
   docker-compose up -d --build
   ```
3. The environment will be available at:
   - **Backend (Swagger UI):** `http://localhost:5137/swagger`
   - **Frontend (Angular):** `http://localhost:4200`
   - **SQL Server:** Mapped to port `1434` on your host.

### 💻 Option B: Running Locally (Manual Setup)

**Prerequisites:** .NET 10 SDK and a local SQL Server instance.

1. Clone the repository and navigate to the project folder.
2. Update the `DefaultConnection` string in `appsettings.json` to point to your local SQL Server.
3. Apply Entity Framework migrations to create the database:
   ```bash
   dotnet ef database update --project Clinica.Infrastructure --startup-project Clinica.API
   ```
4. Run the API:
   ```bash
   dotnet run --project Clinica.API
   ```
5. Access Swagger at: `http://localhost:5xxx/swagger`

## 🔒 Authentication

To access protected endpoints (e.g., patient management):

1. Create a user via the `POST /api/auth/registrar` endpoint.
2. Log in using `POST /api/auth/login` to receive your **JWT Token**.
3. In Swagger, click the 🔓 (Authorize) button and paste the token (the `Bearer ` prefix is handled automatically).

---

Developed by **Lorenzo Zagallo** 👨‍💻
