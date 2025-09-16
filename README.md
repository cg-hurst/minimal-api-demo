# Minimal API Demo

This project demonstrates the use of **ASP.NET Core Minimal APIs** to build a lightweight and modern web API.

---

## Features

- **Minimal API Design**: Lightweight and modular API endpoints.
- **Exception Handling**: Centralized middleware for handling unhandled exceptions.
- **Dependency Injection**: Services like `BookService` are injected into endpoints.
- **Asynchronous Programming**: All endpoints and services are fully asynchronous.
- **Health Checks**: A `/health` endpoint to monitor the application's status.

---

## Prerequisites

- **.NET 9 SDK**: Ensure you have the .NET 9 SDK installed.

---

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/cg-hurst/minimal-api-demo.git
cd minimal-api-demo
```

### 2. Build the Project
```bash
dotnet build
```

### 3. Run the Application
```bash
dotnet run
```

The application will start on `https://localhost:7101`.

---

## API Endpoints

### **Books API**
- **GET /v1/books**: Retrieve all books.
- **GET /v1/books/{id}**: Retrieve a book by its ID.
- **POST /v1/books**: Add a new book.
- **PUT /v1/books**: Update an existing book.
- **DELETE /v1/books/{id}**: Delete a book by its ID.

### **Health Check**
- **GET /v1/health**: Check the application's health status.

---

## Project Structure

- **`Program.cs`**: Configures the application, middleware, and services.
- **`AddBookApiExtension.cs`**: Defines the `Books` API endpoints.
- **`AddHealthApiExtension.cs`**: Defines the `Health` API endpoint.
- **`BookService.cs`**: Provides in-memory data storage and operations for books.
- **`ExceptionHandlerMiddleware.cs`**: Handles unhandled exceptions globally.
