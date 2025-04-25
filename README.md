

## 🚀 Project Highlights

- **Microservices Architecture**: Modular services with clear separation of concerns.
- **Event-Driven Communication**: Powered by RabbitMQ for asynchronous messaging.
- **Resilience & Fault Tolerance**: Integrated Polly for retries, circuit breakers, and fallback.
- **Containerization**: All services are dockerized for consistency and ease of deployment.
- **Scalable Design**: Services can be independently scaled and deployed.
- **Real-World Use Case**: Covers core aspects of an actual eCommerce system (Catalog, Basket, Order, Payment, etc.).

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
| Payment API    | Simulates payment processing |
| Identity API   | Manages authentication and authorization |
| Gateway API    | API gateway for routing and aggregation |
| Event Bus      | Manages integration events using RabbitMQ |

## 🛠️ Features

- Service-to-service communication via REST and events
- Centralized logging and error handling
- Retry and timeout policies with Polly
- Secure APIs with JWT authentication
- Clean architecture with domain-driven design principles

## 📦 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/products/docker-desktop)
- [RabbitMQ](https://www.rabbitmq.com/)
- [Visual Studio or VS Code](https://visualstudio.microsoft.com/)

### Running the Project

```bash
# Clone the repository
git clone https://github.com/your-username/net-microservices-s.git
cd net-microservices-s

# Launch services using Docker Compose
docker-compose up --build
