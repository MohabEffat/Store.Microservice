# 🛒 NET Microservices S


## 🚀 Project Highlights

- **Microservices Architecture**: Modular services with clear separation of concerns.
- **Event-Driven Communication**: Powered by RabbitMQ for asynchronous messaging.
- **Resilience & Fault Tolerance**: Integrated Polly for retries, circuit breakers, and fallback.
- **Containerization**: All services are Dockerized for consistency and ease of deployment.
- **Scalable Design**: Services can be independently scaled and deployed.
- **Real-World Use Case**: Covers core aspects of an actual eCommerce system (Catalog, Basket, Order, etc.).

## 🧩 Tech Stack

- **ASP.NET Core Web API**
- **Docker & Docker Compose**
- **RabbitMQ**
- **Polly (.NET resilience library)**
- **Entity Framework Core**
- **SQL Server / PostgreSQL**
- **Swagger / OpenAPI**
- **RESTful APIs**

## 📦 Microservices Overview

| Service        | Description |
|----------------|-------------|
| Catalog API    | Manages product data |
| Basket API     | Handles shopping cart operations |
| Order API      | Processes customer orders |
| Identity API   | Manages authentication and authorization |
| Gateway API    | API gateway for routing and aggregation |
| Event Bus      | Manages integration events using RabbitMQ |

## 🔧 In Progress

⚠️ **This project is still in development and not yet complete.**  
The following services and features are **planned** and **coming soon**:

- 🧾 **Payment Service** – Process and verify transactions
- 📦 **Inventory Service** – Track stock and availability
- 🔔 **Notification Service** – Email, SMS, and in-app alerts
- 📈 **Monitoring & Logging** – Centralized metrics and dashboards
- 🛡 **Advanced Security** – Role-based access and API rate-limiting

Stay tuned for updates as the project continues to evolve!

## 🛠️ Features

- Service-to-service communication via REST and events
- Centralized logging and error handling
- Retry and timeout policies with Polly
- Secure APIs with JWT authentication
- Clean architecture with domain-driven design principles



### Running the Project

```bash
# Clone the repository
git clone https://github.com/your-username/net-microservices-s.git
cd net-microservices-s

# Launch services using Docker Compose
docker-compose up --build
