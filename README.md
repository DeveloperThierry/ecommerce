# E-Commerce ASP.NET Core API


![Screenshot](public/api.png)
![Screenshot](public/cache.png)
![Screenshot](public/ecommerce.png)

## Short Description

A production-ready, scalable e-commerce backend API built with ASP.NET Core .NET 10, featuring clean architecture, secure authentication, and optimized performance.

## Features

*   **Secure Authentication**: Robust user management with JWT (JSON Web Token) and role-based authorization.
*   **Database Management**: Persistent data access utilizing Entity Framework Core with SQL Server.
*   **Performance Optimization**: Dynamic response caching using In-Memory and Redis for high-traffic scalability.
*   **Professional Logging**: Structured diagnostic tracking via Serilog for easier debugging and monitoring.
*   **Advanced Querying**: Built-in support for API pagination, filtering, and sorting to handle large datasets.

## Installation

1.  **Prerequisites**: Ensure you have .NET 10 SDK and SQL Server installed.

2.  **Clone the repository**: `git clone <your-repo-url>`

3.  **Configure Connection**: Update the `DefaultConnection` string in `appsettings.json` to point to your local SQL Server instance.

4.  **Database Migration**: Run the following commands in the Package Manager Console:
    bash
    Add-Migration InitialCreate
    Update-Database

5.  **Launch**: Run the project using `dotnet run` or via Visual Studio.

## Usage

*   **Swagger UI**: Access the interactive documentation at `/swagger` after starting the application.
*   **Authentication**: Obtain a JWT by calling the `/api/auth/login` endpoint with valid credentials.
*   **Authorization**: Include your token in the request header to access protected endpoints like `/api/products` (POST/PUT/DELETE).

To ensure your `README.md` file reflects the complete development journey from the *ASP.NET Core .NET 10* course, you can incorporate the following **Completed Tasks** section. This provides clear documentation of the technical milestones achieved throughout the project build:

## Tasks Completed

*   **Environment Setup**: Configured the .NET 10 SDK and SQL Server prerequisites for a production-ready environment.
*   **Database Infrastructure**: Implemented **Entity Framework Core** with SQL Server, including full database migration management and data seeding.
*   **Core API Development**: Built a professional **RESTful API** with full CRUD capabilities (Create, Read, Update, Delete) and support for advanced data querying (pagination, filtering, and sorting).
*   **Security Implementation**: Integrated **JWT (JSON Web Token)** for secure user authentication and established granular **role-based authorization** to protect sensitive API endpoints.
*   **Performance Optimization**: Engineered a two-tier caching strategy using **In-Memory caching** for local data and **Redis** for distributed, high-traffic scalability.
*   **Monitoring and Diagnostics**: Deployed **Serilog** for structured, professional logging and enabled comprehensive **Swagger UI** for interactive API documentation and testing.

## Contributing

We welcome community contributions! Please read our CONTRIBUTING.md to understand our workflow and coding standards before submitting a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

-----

*Need help or have questions? Please open an issue in the repository.*



## Testing

### **Authentication Controller**

*   **POST** `/api/auth/register`: Used to create a new user account.
*   **POST** `/api/auth/login`: Used for user authentication to receive a *JWT* (JSON Web Token).

### **Product Controller**

*   **GET** `/api/products`: Retrieves all products, supporting pagination, filtering, and sorting.
*   **GET** `/api/products/{id}`: Retrieves a single product by its unique identifier.
*   **POST** `/api/products`: Creates a new product (restricted to admin role).
*   **PUT** `/api/products/{id}`: Updates an existing product (restricted to admin role).
*   **DELETE** `/api/products/{id}`: Removes a product from the system (restricted to admin role).

### **Cart Controller**

*   **GET** `/api/cart`: Retrieves the shopping cart for the authenticated user.
*   **POST** `/api/cart`: Adds an item to the user's cart.
*   **PUT** `/api/cart`: Updates the quantity of an item in the cart.
*   **DELETE** `/api/cart/{productId}`: Removes an item from the cart.

### **Order Controller**

*   **POST** `/api/orders/checkout`: Processes the checkout for the items currently in the user's cart.


## Highlights

*   **Engineered a scalable RESTful e-commerce backend** using **ASP.NET Core** and **Entity Framework Core**, integrating **PostgreSQL** for relational data and **Redis** for distributed caching to optimize API response times.
*   **Architected secure authentication and authorization systems** utilizing **JSON Web Tokens (JWT)** and **Role-Based Access Control (RBAC)** to enforce granular user and administrative permissions.
*   **Modernized deployment workflows** by implementing **containerized infrastructure** via **Docker**, facilitating consistent environments and streamlining the CI/CD pipeline on *Render*.
*   **Optimized database performance** by configuring **Entity Framework Core** with precision-based **decimal mapping** and query-level filtering to reduce data payload sizes.
*   **Standardized cross-layer communication** by implementing **Data Transfer Objects (DTOs)** and integrating **AutoMapper** to decouple internal domain models from API contracts.
*   **Strengthened application robustness** by replacing basic data annotations with **FluentValidation**, enabling complex, maintainable business rule validation outside of the models.
*   **Enhanced observability and debugging** by integrating **Serilog** for structured logging, capturing request-response lifecycles and critical system warnings to persistent storage.
*   **Improved API performance** through **in-memory caching** and subsequent migration to **distributed Redis caching**, ensuring low-latency retrieval for frequent read operations.