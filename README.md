# SnapShop API

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?logo=dotnet)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-8.0-512BD4?logo=dotnet)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?logo=microsoftsqlserver)
![Redis](https://img.shields.io/badge/Redis-Caching-DC382D?logo=redis)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?logo=docker)

## Overview

**SnapShop API** is an e-commerce backend built with **ASP.NET Core 8** and **Entity Framework Core**, providing RESTful APIs for product browsing, customer accounts, shopping baskets, and order management.

The project was developed as a practical backend implementation while studying advanced ASP.NET Core Web API concepts, with a focus on **separation of concerns, reusable data-access abstractions, flexible querying, authentication, caching, and integration with external services**.

The solution is organized into multiple projects to separate the API, domain abstractions, infrastructure/data-access concerns, and application services.

---

## Key Features

- 🔐 **Authentication & Authorization**
  - ASP.NET Core Identity
  - JWT Bearer Authentication
  - User registration and login
  - Current-user endpoints

- 🛍️ **Product Catalog**
  - Product, Brand, and Type management
  - Filtering
  - Sorting
  - Pagination
  - Product DTOs
  - Related data retrieval through specifications

- 🔎 **Specification Pattern**
  - Encapsulates reusable query criteria
  - Supports filtering, sorting, pagination, and related-data inclusion
  - Separates query construction from repositories and controllers

- 🗄️ **Entity Framework Core**
  - SQL Server persistence
  - Entity configurations
  - Code-first migrations
  - Database seeding
  - Relationship mapping

- 🏗️ **Repository & Unit of Work**
  - Generic Repository abstraction
  - Basket-specific repository
  - Unit of Work for coordinating data operations

- 🛒 **Shopping Basket**
  - Basket persistence using Redis
  - Basket retrieval, update, and deletion
  - Customer basket validation

- 🔄 **DTO Mapping**
  - AutoMapper
  - Separation between API contracts and domain entities
  - Custom product image URL resolution

- ⚠️ **Centralized Error Handling**
  - Global exception middleware
  - Consistent API error responses
  - Validation error handling
  - Not-found and bad-request responses

- 📦 **Order Management**
  - Order domain model
  - Order items
  - Delivery methods
  - Order service layer
  - User-specific order retrieval

- 💳 **Payment Integration**
  - Payment intent creation/update
  - Payment-related order workflow
  - Webhook-based payment confirmation

- 📚 **API Documentation**
  - Swagger / OpenAPI
  - JWT authentication support for API testing

- 🐳 **Docker Support**
  - Dockerfile-based containerization
  - Container-oriented application configuration

---

# Architecture

The solution follows a **layered architecture inspired by Clean Architecture principles**, with responsibilities separated across multiple projects.

```text
                    ┌─────────────────────────┐
                    │      SnapShop.APIs      │
                    │  Controllers / DTOs /   │
                    │ Middleware / API Layer  │
                    └────────────┬────────────┘
                                 │
                                 ▼
                    ┌─────────────────────────┐
                    │     SnapShop.Service    │
                    │  Application Services   │
                    │  Token / Order Logic    │
                    └────────────┬────────────┘
                                 │
                                 ▼
                    ┌─────────────────────────┐
                    │      SnapShop.Core      │
                    │ Entities / Interfaces / │
                    │ Specifications / Models │
                    └────────────┬────────────┘
                                 │
                                 ▼
                    ┌─────────────────────────┐
                    │   SnapShop.Repository   │
                    │ EF Core / SQL Server /  │
                    │ Redis / Repositories    │
                    └─────────────────────────┘
```

### Project Responsibilities

#### `SnapShop.APIs`

Presentation layer responsible for:

- Controllers
- DTOs
- API response models
- Authentication configuration
- Middleware
- API-specific helpers
- Swagger/OpenAPI configuration

#### `SnapShop.Core`

Contains the application's core abstractions and domain models:

- Product entities
- Basket entities
- Identity models
- Order entities
- Repository interfaces
- Service interfaces
- Unit of Work abstraction
- Specification definitions

The Core project is designed to remain independent from infrastructure implementations.

#### `SnapShop.Repository`

Infrastructure and persistence layer responsible for:

- Entity Framework Core
- SQL Server database access
- DbContext
- Entity configurations
- Database migrations
- Data seeding
- Generic Repository implementation
- Specification evaluation
- Unit of Work implementation
- Redis basket repository
- ASP.NET Identity database context

#### `SnapShop.Service`

Application-level services responsible for business operations that do not belong directly inside controllers or repositories.

Current examples include:

- `OrderService`
- `TokenService`

---

# Technology Stack

| Technology | Purpose |
|---|---|
| **C#** | Primary programming language |
| **.NET 8** | Application platform |
| **ASP.NET Core Web API** | REST API framework |
| **Entity Framework Core** | ORM and data access |
| **SQL Server** | Relational database |
| **ASP.NET Core Identity** | User and identity management |
| **JWT Bearer** | Stateless API authentication |
| **Redis** | Shopping basket persistence/cache |
| **AutoMapper** | DTO/entity mapping |
| **Swagger / OpenAPI** | API documentation and testing |
| **Docker** | Application containerization |

---

# Core Design Patterns

## Generic Repository

A reusable repository abstraction is used for common data-access operations.

This keeps common persistence operations behind an abstraction and allows higher layers to work with repository interfaces rather than directly depending on concrete repository implementations.

## Specification Pattern

The project uses specifications to encapsulate query logic.

Specifications are used to build reusable queries involving:

- Filtering
- Sorting
- Pagination
- Related entity inclusion

This keeps complex query construction out of controllers and makes query requirements easier to compose and reuse.

## Unit of Work

A Unit of Work abstraction coordinates repository operations and provides a single point for committing related changes.

This is particularly useful in workflows involving multiple repository operations that should be persisted together.

---

# Product Catalog

The product API supports common e-commerce catalog operations including:

- Product listing
- Product details
- Filtering
- Sorting
- Pagination
- Product type lookup
- Product brand lookup

Product responses are exposed through DTOs rather than returning persistence entities directly.

A custom URL resolver is also used to construct product image URLs returned to API clients.

---

# Authentication

The project uses **ASP.NET Core Identity** for user management and **JWT Bearer Authentication** for API authentication.

The authentication flow includes:

```text
Client
  │
  │ Register / Login
  ▼
ASP.NET Core API
  │
  ├── Identity
  │     └── User management
  │
  └── Token Service
          │
          ▼
      JWT Token
          │
          ▼
Authenticated API Requests
```

Protected endpoints can use the generated JWT to identify and authorize the current user.

---

# Shopping Basket

The shopping basket is stored using **Redis** rather than the relational SQL Server database.

This separates short-lived basket state from the application's relational business data.

The basket functionality includes:

- Retrieve basket
- Create/update basket
- Delete basket
- Validate customer basket
- Persist basket state in Redis

This also provides a practical example of integrating a distributed in-memory data store into an ASP.NET Core API.

---

# Order Management

The project contains a dedicated order domain including:

- Orders
- Order Items
- Products associated with orders
- Delivery Methods
- Order Status

Order-related business operations are handled through an application service rather than placing the complete workflow inside controllers.

The order workflow includes operations for:

- Creating orders
- Retrieving orders for the current user
- Retrieving an individual user order
- Retrieving available delivery methods
- Associating payment information with orders

---

# Payment Workflow

The project integrates payment processing through a **payment-intent based workflow**.

A simplified flow is:

```text
Client
  │
  ▼
Create Order
  │
  ▼
Create / Update Payment Intent
  │
  ▼
Payment Provider
  │
  ▼
Payment Confirmation
  │
  ▼
Webhook
  │
  ▼
API
  │
  ▼
Update Order Payment State
```

Using a webhook allows the backend to receive payment-provider events independently of the original client request.

This is important because payment confirmation should not rely solely on the client reporting that a payment succeeded.

---

# Error Handling

The API implements centralized exception handling through custom middleware.

The application distinguishes between different types of API failures, including:

- Resource not found
- Invalid requests
- Validation failures
- Unexpected server exceptions

Standardized response models are used to keep error responses consistent across the API.

---

# Database

The application uses **SQL Server** with **Entity Framework Core**.

Database responsibilities include:

- Entity configurations
- Relationships
- Constraints
- Code-first migrations
- Database seeding

The repository layer contains the database context and migration history.

Two database contexts are used for separating application data from Identity-related persistence.

---

# API Documentation

Swagger / OpenAPI is configured for exploring and testing the API.

After starting the application, Swagger can be accessed through:

```text
https://localhost:<port>/swagger
```

When authentication is enabled, JWT bearer authentication can be supplied through Swagger to test protected endpoints.

---

# Getting Started

## Prerequisites

Make sure the following are installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server
- Redis
- Git
- Docker (optional)

## Clone the Repository

```bash
git clone https://github.com/AboAlsho2/SnapShopAPI.git

cd SnapShopAPI
```

## Configure the Application

Update the connection strings and other configuration values in:

```text
SnapShop.APIs/appsettings.json
```

Example structure:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<SQL_SERVER_CONNECTION_STRING>",
    "IdentityConnection": "<IDENTITY_DATABASE_CONNECTION_STRING>",
    "Redis": "<REDIS_CONNECTION>"
  }
}
```

> Do not commit real passwords, connection strings, JWT secrets, or other sensitive configuration values to source control.

## Start Redis

If Redis is running through Docker:

```bash
docker run -d --name snapshop-redis -p 6379:6379 redis
```

## Run the API

```bash
dotnet restore

dotnet build

dotnet run --project SnapShop.APIs
```

Then open Swagger:

```text
https://localhost:<port>/swagger
```

---

# Docker

The API includes Docker support through a dedicated Dockerfile.

A basic image build can be performed with:

```bash
docker build -t snapshop-api -f SnapShop.APIs/Dockerfile .
```

Run the container:

```bash
docker run -p 8080:8080 snapshop-api
```

For a complete containerized environment, the API also requires its external dependencies such as SQL Server and Redis to be configured appropriately.

---

# Project Structure

```text
SnapShopAPI/
│
├── SnapShop.APIs/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Errors/
│   ├── Helpers/
│   ├── Middlewares/
│   ├── wwwroot/
│   ├── Program.cs
│   └── Dockerfile
│
├── SnapShop.Core/
│   ├── Models/
│   ├── Repositories/
│   ├── Services/
│   ├── Specifications/
│   └── IUnitOfWork.cs
│
├── SnapShop.Repository/
│   ├── Data/
│   │   ├── Configurations/
│   │   ├── DataSeed/
│   │   ├── Migrations/
│   │   └── ShopContext.cs
│   ├── Identity/
│   ├── BasketRepository.cs
│   ├── GenericRepository.cs
│   ├── SpecificationEvaluator.cs
│   └── UnitOfWork.cs
│
└── SnapShop.Service/
    ├── OrderService.cs
    └── TokenService.cs
```

---

# API Areas

The API is organized around the main e-commerce domains:

| Area | Responsibility |
|---|---|
| **Accounts** | Registration, login, and current-user operations |
| **Products** | Product catalog, filtering, sorting, and pagination |
| **Basket** | Customer shopping basket management |
| **Orders** | Order creation and retrieval |
| **Delivery** | Available delivery methods |
| **Payments** | Payment intent and payment confirmation workflow |

> Endpoint names and authorization requirements may vary depending on the current implementation/configuration.

---

# Engineering Focus

The main engineering goals of this project were:

- Separation of concerns
- Maintainable API structure
- Reusable data-access abstractions
- Flexible database querying
- Secure authentication
- Distributed basket storage with Redis
- Consistent API error handling
- DTO-based API contracts
- Business logic separation through application services
- Practical integration with external payment services
- Containerization with Docker

---

# Future Improvements

Potential improvements for a production-oriented version include:

- Automated unit and integration test coverage
- Centralized configuration and secret management
- Structured logging and distributed tracing
- API versioning
- Rate limiting
- Health checks
- CI/CD pipeline
- Container orchestration
- More comprehensive observability
- Improved automated security testing
- Additional caching strategies where justified

---

# Author

**Mohamed Shawky (AboAlsho2)**

GitHub: [github.com/AboAlsho2](https://github.com/AboAlsho2)

---

## Disclaimer

This project was developed as a practical learning and portfolio project while studying advanced ASP.NET Core Web API and backend development concepts.

The implementation focuses on applying backend architecture, data access, authentication, caching, order processing, and external service integration concepts in a realistic e-commerce scenario.
